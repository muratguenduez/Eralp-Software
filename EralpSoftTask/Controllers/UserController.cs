using EralpSoftTask.Data;
using EralpSoftTask.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace EralpSoftTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //private readonly DataContext _context;

        //public UserController(DataContext context)
        //{
        //    _context = context;
        //}

        //// your controller actions here

        [HttpGet]
        [Route("user")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<UserModel>))]
        public IActionResult getUser(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var response = new { status = "success", message = "Object created" };

                // Serialize the response object into JSON
                var json = JsonConvert.SerializeObject(response);

                // Return the serialized JSON with a 200 OK status code
                return Ok(json);
            }
            return Ok();
        }

    }
}
