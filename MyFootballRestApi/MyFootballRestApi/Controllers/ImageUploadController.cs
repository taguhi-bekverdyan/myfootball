using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
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
        private Cloudinary cloudinary;

        public ImageUploadController()
        {
            var account = new Account(
                "myfootball-am",
                "146315763856442",
                "39tiuvYatl-1kXLVIMifY1nfSuQ");

            cloudinary = new Cloudinary(account);
        }

        // POST: api/Image
        [HttpPost]
        public async Task<IActionResult> Post(IFormFile file)
        {
            var fileName = Guid.NewGuid() + DateTime.Now.ToString("yyyyMMddHHmmssfff");
            using (Stream stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(fileName, stream),
                    PublicId = fileName
                };
                var uploadResult = cloudinary.Upload(uploadParams);
                return Ok(uploadResult.SecureUri.AbsoluteUri);
            }
        }


    }
}