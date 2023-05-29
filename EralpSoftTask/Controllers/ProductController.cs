using EralpSoftTask.Data;
using EralpSoftTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace EralpSoftTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly SqlDataContext _dbContext;
        public ProductController(SqlDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _dbContext.tblProduct.ToListAsync();
            if (result == null)
                return null;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var result = await _dbContext.tblProduct.FindAsync(id);
            if (result == null) 
                return null;

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(ProductModel request)
        {
            _dbContext.tblProduct.Add(request);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.tblProduct.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _dbContext.tblProduct.FindAsync(id);
            if (result == null) 
                return null;

            _dbContext.tblProduct.Remove(result);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.tblProduct.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, ProductModel request)
        {
            var result = await _dbContext.tblProduct.FindAsync(id);
            if (result == null)  
                return null; 

            result.name = request.name;
            result.description = request.description;
            result.price = request.price;
            result.instock = request.instock;
            result.userid = request.userid;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.tblProduct.ToListAsync());
        }

        
    }
}
