using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Threading.Tasks;
using System.Linq;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("GetProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _dbContext.Products
                .Select(p => new Product
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Price = p.Price
                }).ToListAsync();
            
            return Ok(products);
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest("Invalid product data.");
            }
            
            await _dbContext.Products.AddAsync(newProduct);
            await _dbContext.SaveChangesAsync();
            
            return CreatedAtAction(nameof(GetAllProducts), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> ModifyProduct([FromBody] Product updatedProduct)
        {
            if (!_dbContext.Products.Any(p => p.Id == updatedProduct.Id))
            {
                return NotFound();
            }
            
            _dbContext.Products.Update(updatedProduct);
            await _dbContext.SaveChangesAsync();
            
            return NoContent();
        }

        [HttpDelete("DeleteProduct/{id}")]
        public async Task<IActionResult> RemoveProduct(int id)
        {
            var existingProduct = await _dbContext.Products.FindAsync(id);
            if (existingProduct == null)
            {
                return NotFound();
            }
            
            _dbContext.Products.Remove(existingProduct);
            await _dbContext.SaveChangesAsync();
            
            return NoContent();
        }
    }
}