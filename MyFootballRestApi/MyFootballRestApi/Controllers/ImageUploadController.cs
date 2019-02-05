using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {

        private readonly IHostingEnvironment _environment;
        public ImageUploadController(IHostingEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
        }

        // POST: api/Image
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            //var uploads = Path.Combine(_environment.WebRootPath, "uploads");

            var uploads = $@"D:\MyFootball\uploads";
            if (file.Length > 0)
            {
                var ext = Path.GetExtension(file.FileName);
                try
                {
                    var fileName = Guid.NewGuid() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    using (var fileStream = new FileStream(Path.Combine(uploads, fileName+ext), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                    return Ok(Path.Combine(uploads, fileName + ext));
                }
                catch (Exception e)
                {
                    return StatusCode(500, e);
                }
            }
            return BadRequest();
        }


    }
}