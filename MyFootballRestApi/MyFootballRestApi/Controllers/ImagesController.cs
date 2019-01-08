using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                byte[] b = System.IO.File.ReadAllBytes(@"D:\myfootball\MyFootballRestApi\MyFootballRestApi\Images\icon-user-default.png");         
                return File(b, "image/jpeg");
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

    }
}