using EralpSoftTask.Data;
using EralpSoftTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EralpSoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SqlDataContext _dbContext;
        public UserController(SqlDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var result = await _dbContext.tblUser.ToListAsync();
            if (result == null)
                return null;

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var result = await _dbContext.tblUser.FindAsync(id);
            if (result == null)
                return null;

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddUsers(UserModel request)
        {
            _dbContext.tblUser.Add(request);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.tblUser.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(int id)
        {
            var result = await _dbContext.tblUser.FindAsync(id);
            if (result == null)
                return null;

            _dbContext.tblUser.Remove(result);
            await _dbContext.SaveChangesAsync();
            return Ok(await _dbContext.tblUser.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatelUser(int id, UserModel request)
        {
            var result = await _dbContext.tblUser.FindAsync(id);
            if (result == null)
                return null;

            result.username = request.username;
            result.password = request.password;
            result.email = request.email;            
            result.firstname = request.firstname;
            result.lastname = request.lastname;

            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.tblUser.ToListAsync());
        }

    }
}
