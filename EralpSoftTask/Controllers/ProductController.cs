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
            var result = await _dbContext.tblProduct.Include(p => p.User).ToListAsync();

            if (result == null)
                return null;

            //var products = from p in _dbContext.tblProduct
            //               join u in _dbContext.tblUser on p.userid equals u.id
            //               select new ProductModel
            //               {
            //                   id = p.id,
            //                   name = p.name,
            //                   description = p.description,
            //                   price = p.price,
            //                   instock = p.instock,
            //                   userid = p.userid,
            //                   user = new UserModel
            //                   {
            //                       id = u.id,
            //                       username = u.username,
            //                       firstname = u.firstname,
            //                       lastname = u.lastname,
            //                       email = u.email
            //                   }
            //               };

            //return Ok(products);

            var modifiedResult = result.Select(r => new            
            {
                r.id,
                r.name,
                r.description,
                r.price,
                r.instock,
                r.userid,
                User = new
                {
                    r.User.firstname,
                    r.User.lastname,
                    r.User.username
                }
            });

            return Ok(modifiedResult);            
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
