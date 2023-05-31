using EralpSoftTask.Data;
using EralpSoftTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EralpSoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SqlDataContext _dbContext;
        public UserController(IConfiguration configuration, SqlDataContext dbContext)
        {
            _configuration = configuration;
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

        [HttpPost("register")]
        public async Task<IActionResult> AddUsers(UserModel request)
        {
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(request.password);
            request.password = passwordHash;
            _dbContext.tblUser.Add(request);
            await _dbContext.SaveChangesAsync();

            return Ok(await _dbContext.tblUser.ToListAsync());
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(User request)
        {
            var users = await _dbContext.tblUser.ToListAsync();

            var user = from u in users
                       where u.username == request.Username
                       select u;
            var listUsers = user.ToList();           

            if (listUsers[0].username != request.Username)
            {
                return BadRequest("User not found.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.PasswordHash, listUsers[0].password))
            {
                return BadRequest("Wrong Password.");
            }

            string token = CreateToken(listUsers[0]);

            return Ok(token);
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

        private string CreateToken(UserModel user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;

        }
    }
}
