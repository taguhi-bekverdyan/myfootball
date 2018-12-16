using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Controllers
{
    public class TestController : Controller
    {
        private async Task<string> GetAccessToken()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");

                // if you need to check the access token expiration time, use this value
                // provided on the authorization response and stored.
                // do not attempt to inspect/decode the access token
                var accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);

                var idToken = await HttpContext.GetTokenAsync("id_token");

                return accessToken;

                // Now you can use them. For more info on when and how to use the 
                // access_token and id_token, see https://auth0.com/docs/tokens
            }
            return string.Empty;

        }

        public async Task<IActionResult> Index()
        {
            return View();
        }


        public async Task<IActionResult> TestPublic()
        {
            return StatusCode(404);

            var client = new RestClient("https://localhost:44350/api/test/public");
            var request = new RestRequest(Method.GET);
            //request.AddHeader("authorization", "Bearer YOUR_ACCESS_TOKEN");
            request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
            var response = client.Execute(request);

            dynamic json = JsonConvert.DeserializeObject(response.Content);

            ViewData["Message"] = json.message;

            return View("~/Views/Test/Test.cshtml");
        }

        public async Task<IActionResult> TestPrivate()
        {
            return StatusCode(500);

            var client = new RestClient("https://localhost:44350/api/test/private");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
            var response = client.Execute(request);

            dynamic json = JsonConvert.DeserializeObject(response.Content);

            ViewData["Message"] = json.message;

            return View("~/Views/Test/Test.cshtml");
        }

        public async Task<IActionResult> TestUser()
        {
            return StatusCode(503);

            var client = new RestClient("https://localhost:44350/api/test/private-user");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
            var response = client.Execute(request);

            dynamic json = JsonConvert.DeserializeObject(response.Content);

            ViewData["Message"] = json.message;

            return View("~/Views/Test/Test.cshtml");
        }

        public async Task<IActionResult> TestAdmin()
        {
            return StatusCode(502);

            var client = new RestClient("https://localhost:44350/api/test/private-admin");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
            var response = client.Execute(request);

            dynamic json = JsonConvert.DeserializeObject(response.Content);

            ViewData["Message"] = json.message;

            return View("~/Views/Test/Test.cshtml");
        }

        public async Task<IActionResult> Claims()
        {
            var client = new RestClient("https://localhost:44350/api/test/claims");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
            var response = client.Execute(request);

            dynamic json = JsonConvert.DeserializeObject(response.Content);

            ViewData["Message"] = response.Content;

            return View("~/Views/Test/Test.cshtml");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}