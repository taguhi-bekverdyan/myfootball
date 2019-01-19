using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MyFootballRestApi.Models;

namespace MyFootballRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : Controller
    {
        private readonly IEmailSender _emailSender;

        public EmailController(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        #region POST
        [HttpPost("SendEmail")]
        public async Task<IActionResult> SendEmail([FromBody] Email email)
        {
            try
            {

                await _emailSender.SendEmailAsync(email.Address, email.Subject, email.Message);
                return Ok("OK");
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }
        #endregion
    }
}