using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return new JsonResult(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }


        // GET api/values
        [HttpGet]
        [Route("private")]
        [Authorize]
        public IActionResult Private()
        {
            return new JsonResult(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }

        [HttpGet("claims")]
        public IActionResult Claims()
        {
            return new JsonResult(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }

    }
}
