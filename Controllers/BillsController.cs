using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/bills")]
    public class BillsController : ControllerBase
    {
        private readonly IMongoCollection<Bill> _billCollection;
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<StockEntry> _stockCollection;

        public BillsController(IMongoDatabase database)
        {
            _billCollection = database.GetCollection<Bill>("Bills");
            _productCollection = database.GetCollection<Product>("Products");
            _stockCollection = database.GetCollection<StockEntry>("StockEntries");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBill(BillCreateDto dto)
        {
            // Validation: At least one item
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("At least one item is required");

            // Generate invoice number
            var lastBill = await _billCollection
                .Find(_ => true)
                .SortByDescending(x => x.InvoiceNumber)
                .Limit(1)
                .FirstOrDefaultAsync();

            int invoiceNumber = lastBill == null ? 1001 : lastBill.InvoiceNumber + 1;

            decimal totalAmount = 0;
            var billItems = new List<BillItem>();

            foreach (var item in dto.Items)
            {
                // Basic validation
                if (item.QuantityKg <= 0 || item.PricePerKg <= 0)
                    return BadRequest($"Invalid quantity or price for {item.Description}");

                if (item.DiscountPercent < 0 || item.DiscountPercent > 100)
                    return BadRequest($"Invalid discount for {item.Description}");

                // --- STOCK CALCULATION USING LINQ ---
                // Total received for this product
                var allStock = await _stockCollection.Find(_ => true).ToListAsync();
                decimal totalStockInKg = allStock
                    .SelectMany(s => s.Items)
                    .Where(i => i.ProductId == item.ProductId)
                    .Sum(i => i.QuantityKg);

                // Total sold for this product
                var allBills = await _billCollection.Find(_ => true).ToListAsync();
                decimal totalSoldKg = allBills
                    .SelectMany(b => b.Items)
                    .Where(i => i.ProductId == item.ProductId)
                    .Sum(i => i.QuantityKg);

                decimal available = totalStockInKg - totalSoldKg;

                if (item.QuantityKg > available)
                    return BadRequest($"Not enough stock for {item.Description}. Available: {available} kg");

                // --- CALCULATION ---
                var gross = item.QuantityKg * item.PricePerKg;
                var discountAmount = gross * (item.DiscountPercent / 100);
                var net = gross - discountAmount;

                totalAmount += net;

                billItems.Add(new BillItem
                {
                    ProductId = item.ProductId,
                    Description = item.Description,
                    QuantityKg = item.QuantityKg,
                    PricePerKg = item.PricePerKg,
                    DiscountPercent = item.DiscountPercent
                });
            }

            // Create bill
            var bill = new Bill
            {
                InvoiceNumber = invoiceNumber,
                RestaurantName = dto.RestaurantName,
                InvoiceDate = dto.InvoiceDate,
                Items = billItems,
                TotalAmount = totalAmount
            };

            await _billCollection.InsertOneAsync(bill);

            return Ok(new
{
                bill.InvoiceNumber,
    bill.RestaurantName,
    bill.InvoiceDate,
    Items = billItems.Select(i => new
    {
        i.ProductId,
        i.Description,
        i.QuantityKg,
        i.PricePerKg,          // returned
        i.DiscountPercent,
        Amount = i.QuantityKg * i.PricePerKg
                 - ((i.QuantityKg * i.PricePerKg) * i.DiscountPercent / 100)
    }),
    TotalAmount = totalAmount
});
        }
        [HttpGet]
        public async Task<IActionResult> GetBills()
        {
            var bills = await _billCollection.Find(_ => true).ToListAsync();
            return Ok(bills);
        }

    }

}
