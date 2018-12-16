using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult>  Index()
        {


            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            string message = "";

            if(statusCode != null)
            {
                switch(statusCode)
                {
                    case 404:
                        {
                            message = "404 - THE PAGE CAN'T BE FOUND";
                            break;
                        }
                    case 500:
                        {
                            message = "500 - IT'S NOT YOUR FAULT, SOMETHING JUST ISN'T RIGHT";
                            break;
                        }
                    case 503:
                        {
                            message = "503 - LOOKS LIKE WE'RE HAVING SOME SRVER ISSUES";
                            break;
                        }
                    default:
                        {
                            message = "AN UNEXPECTED ERROR SEEMS TO HAVE OCCURED";
                            break;
                        }
                }
            }

            return View(new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
