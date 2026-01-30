using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IMongoCollection<Product> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

        public ProductsController(IMongoDatabase database)
        {
            _productCollection = database.GetCollection<Product>("Products");
            _categoryCollection = database.GetCollection<Category>("Categories");
        }

        // GET: api/products
        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productCollection.Find(_ => true).ToListAsync();
            var categories = await _categoryCollection.Find(_ => true).ToListAsync();

            var result = products.Select(p =>
            {
                var category = categories.FirstOrDefault(c => c.Id == p.CategoryId);
                return new ProductReadDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = category?.Name ?? "Unknown"
                };
            }).ToList();

            return Ok(result);
        }

        // POST: api/products
        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Product name is required");

            // Optional: check if category exists
            var categoryExists = await _categoryCollection.Find(c => c.Id == dto.CategoryId).AnyAsync();
            if (!categoryExists)
                return BadRequest("Invalid category ID");

            var product = new Product
            {
                Name = dto.Name.Trim(),
                CategoryId = dto.CategoryId
            };

            await _productCollection.InsertOneAsync(product);
            return Ok(product);
        }
    }
}

