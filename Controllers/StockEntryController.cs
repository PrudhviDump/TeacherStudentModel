using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using BackEnd.Models;
using BackEnd.DTOs;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/stockentries")]
    public class StockEntryController : ControllerBase
    {
        private readonly IMongoCollection<StockEntry> _stockCollection;
        private readonly IMongoCollection<Product> _productCollection;

        public StockEntryController(IMongoDatabase database)
        {
            _stockCollection = database.GetCollection<StockEntry>("StockEntries");
            _productCollection = database.GetCollection<Product>("Products");
        }

        // GET: api/stockentries
        [HttpGet]
        public async Task<IActionResult> GetAllStockEntries()
        {
            var entries = await _stockCollection.Find(_ => true).ToListAsync();
            return Ok(entries);
        }

        // POST: api/stockentries
        [HttpPost]
        public async Task<IActionResult> CreateStockEntry(StockEntryCreateDto dto)
        {
            // Validate at least one item
            if (dto.Items == null || !dto.Items.Any())
                return BadRequest("At least one stock item is required");

            // Validate product existence
            foreach (var item in dto.Items)
            {
                var exists = await _productCollection.Find(p => p.Id == item.ProductId).AnyAsync();
                if (!exists)
                    return BadRequest($"Product with ID {item.ProductId} does not exist");
            }

            var stockEntry = new StockEntry
            {
                ReceivedDate = DateTime.UtcNow,
                TotalAmountSpent = dto.TotalAmountSpent,
                Items = dto.Items.Select(i => new StockEntryItem
                {
                    ProductId = i.ProductId,
                    QuantityKg = i.QuantityKg
                }).ToList()
            };

            await _stockCollection.InsertOneAsync(stockEntry);

            return Ok(stockEntry);
        }
    }
}
