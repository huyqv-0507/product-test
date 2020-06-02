using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductTest.Models;

namespace ProductTest.Controllers
{
    [Route("api/v3/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext dbContext;
        public ProductsController(ProductDbContext context)
        {
            dbContext = context;
        }
        //GET api/v3/products
        [HttpGet]
        public IActionResult getProducts()
        {
            var products = dbContext.Products.ToList();
            if (products == null) return NotFound();

            return Ok(products);
        }

        //GET api/v3/products/productId
        [HttpGet("{productId}")]
        public IActionResult getProductById(string productId)
        {
            var product = dbContext.Products.Find(productId);
            if (product == null) return NotFound();

            return Ok(product);
        }
        //POST api/v3/products
        [HttpPost]
        public IActionResult createProduct(Product product)
        {
            if (!ValidId(product.ProductId)) return StatusCode(400, "Id must be less than 6");
            dbContext.Products.Add(product);
            try
            {
                dbContext.SaveChanges();
            } catch
            {
                if (ProductIsExisted(product.ProductId))
                    return StatusCode(400, "Prorduct is existed");
            }
            return Ok(product);
        }

        //PUT api/v3/products/productId
        [HttpPut("{productId}")]
        public IActionResult updateProduct(string productId, Product product)
        {
            if (productId != product.ProductId) return NotFound();
            var productdb = dbContext.Products.Find(productId);
            if (productdb == null) return NotFound();
            productdb.ProductName = product.ProductName;
            productdb.Price = product.Price;
            productdb.Description = product.Description;
            try
            {
                dbContext.SaveChanges();
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
            return NoContent();
        }

        //DELETE api/v3/products/productId
        [HttpDelete("{productId}")]
        public IActionResult deleteProduct(string productId)
        {
            var product = dbContext.Products.Find(productId);
            if (product == null) return NotFound();
            dbContext.Products.Remove(product);
            try
            {
                dbContext.SaveChanges();
            } catch(Exception e)
            {
                return NotFound(e.Message);
            }
            
            return Ok(product);
        }
        public bool ProductIsExisted(string productId)
        {
            return dbContext.Products.Any(product => product.ProductId == productId);
        }
        public bool ValidId(string id)
        {
            return id.Length <= 6;
        }
    }
}
