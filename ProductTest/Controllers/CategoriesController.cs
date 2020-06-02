using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProductTest.Models;

namespace ProductTest.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ProductDbContext dbContext;
        public CategoriesController(ProductDbContext context)
        {
            dbContext = context;
        }
        //GET api/v3/categories
        [HttpGet]
        public IActionResult getCategories()
        {
            var categories = dbContext.Categories.ToList();
            if (categories == null) return NotFound();
            return Ok(categories);
        }

        //GET api/v3/categories/categoryId
        [HttpGet("{categoryId}")]
        public IActionResult getCategoriesById(string categoryId)
        {
            var category = dbContext.Categories.Find(categoryId);
            if (category == null) return NotFound();
            return Ok(category);
        }

        //POST api/v3/categories/category
        [HttpPost]
        public IActionResult createCategory(Category category)
        {
            if (!ValidId(category.CategoryId)) return StatusCode(400, "Id must be less than 6");
            dbContext.Add(category);
            try
            {
                dbContext.SaveChanges();
            } catch
            {
                if (CategoryExists(category.CategoryId))
                {
                    return StatusCode(400, "Category is existed");
                }
                else
                {
                    throw;
                }
            }
            
            return Ok(category);
        }

        //PUT api/v3/categories/categoryId
        [HttpPut("{categoryId}")]
        public IActionResult updateCategory(string categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return BadRequest();
            var cate = dbContext.Categories.Find(categoryId);
            if (cate == null) return NotFound();
            cate.CategoryName = category.CategoryName;

            try
            {
                dbContext.SaveChanges();
            } catch(Exception e)
            {
                 return NotFound(e.Message);
            }

            return NoContent();
        }

        //DELETE api/v3/categories/categoryId
        [HttpDelete("{categoryId}")]
        public IActionResult deleteCategory(string categoryId)
        {
            var category = dbContext.Categories.Find(categoryId);
            if (category == null) return NotFound();
            dbContext.Categories.Remove(category);
            try
            {
                dbContext.SaveChanges();
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
            return Ok(category);
        }
        //Check existed category
        private bool CategoryExists(string id)
        {
            return dbContext.Categories.Any(category => category.CategoryId == id);
        }
        public bool ValidId(string id)
        {
            return id.Length <= 6;
        }
    }
}
