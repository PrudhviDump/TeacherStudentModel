using BackEnd.DTOs;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace BackEnd.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMongoCollection<Category> _categoryCollection;

        public CategoriesController(IMongoDatabase database)
        {
            _categoryCollection = database.GetCollection<Category>("Categories");
        }

        // GET: api/categories
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryCollection.Find(_ => true).ToListAsync();
            return Ok(categories);
        }

        // POST: api/categories
        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("Category name is required");

            var category = new Category { Name = dto.Name.Trim() };
            await _categoryCollection.InsertOneAsync(category);

            return Ok(category);
        }
    }
}

