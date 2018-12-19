using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly ILogger<TestController> _logger;

        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            _logger.LogInformation("public endpoint :) :) :) ");
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


        [HttpGet]
        [Route("private-user")]
        [Authorize(Policy = "User")]
        public IActionResult PrivateUser()
        {
            return new JsonResult(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated as USER to see this."
            });
        }

        [HttpGet]
        [Route("private-admin")]
        [Authorize(Policy = "Admin")]
        public IActionResult PrivateAdmin()
        {
            return new JsonResult(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated as ADMIN to see this."
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
