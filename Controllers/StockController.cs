using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockController : ControllerBase
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<StockEntry> _stockCollection;
        private readonly IMongoCollection<Bill> _billCollection;

        public StockController(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
            _stockCollection = database.GetCollection<StockEntry>("StockEntries");
            _billCollection = database.GetCollection<Bill>("Bills");
        }

        [HttpGet("balance")]
        public async Task<IActionResult> GetStockBalance()
        {
            var products = await _productCollection.Find(_ => true).ToListAsync();
            var stockEntries = await _stockCollection.Find(_ => true).ToListAsync();
            var bills = await _billCollection.Find(_ => true).ToListAsync();

            var balances = products.Select(p =>
            {
                var totalReceived = stockEntries
                    .SelectMany(se => se.Items)
                    .Where(i => i.ProductId == p.Id)
                    .Sum(i => i.QuantityKg);

                var totalSold = bills
                    .SelectMany(b => b.Items)
                    .Where(i => i.ProductId == p.Id)
                    .Sum(i => i.QuantityKg);

                return new StockBalanceDto
                {
                    ProductId = p.Id,
                    ProductName = p.Name,
                    TotalReceivedKg = totalReceived,
                    TotalSoldKg = totalSold,
                    AvailableKg = totalReceived - totalSold
                };
            }).ToList();

            return Ok(balances);
        }
    }
}
