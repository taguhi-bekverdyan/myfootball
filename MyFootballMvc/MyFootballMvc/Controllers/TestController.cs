using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Controllers
{
  public class TestController : Controller
  {

    private EmailService _emailService { get; set; }

    public TestController()
    {
      _emailService = new EmailService();
    }

    public async Task<IActionResult> Index()
    {
      TestViewModel viewModel = await GetViewModel();
      viewModel.Email = new Email();
      viewModel.ActiveMenuItem = "test";
      return View("Index", viewModel);
    }


    public async Task<IActionResult> TestPublic()
    {
      var client = new RestClient("https://localhost:44350/api/test/public");
      var request = new RestRequest(Method.GET);
      //request.AddHeader("authorization", "Bearer YOUR_ACCESS_TOKEN");
      request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
      var response = client.Execute(request);

      dynamic json = JsonConvert.DeserializeObject(response.Content);

      ViewData["Message"] = json.message;

      return View("~/Views/Test/Test.cshtml", await GetViewModel());
    }

    public async Task<IActionResult> TestPrivate()
    {
      var client = new RestClient("https://localhost:44350/api/test/private");
      var request = new RestRequest(Method.GET);
      request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
      var response = client.Execute(request);

      dynamic json = JsonConvert.DeserializeObject(response.Content);

      ViewData["Message"] = json.message;

      return View("~/Views/Test/Test.cshtml", await GetViewModel());
    }

    public async Task<IActionResult> TestUser()
    {
      var client = new RestClient("https://localhost:44350/api/test/private-user");
      var request = new RestRequest(Method.GET);
      request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
      var response = client.Execute(request);

      dynamic json = JsonConvert.DeserializeObject(response.Content);

      ViewData["Message"] = json.message;

      return View("~/Views/Test/Test.cshtml", await GetViewModel());
    }

    public async Task<IActionResult> TestAdmin()
    {
      var client = new RestClient("https://localhost:44350/api/test/private-admin");
      var request = new RestRequest(Method.GET);
      request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
      var response = client.Execute(request);

      dynamic json = JsonConvert.DeserializeObject(response.Content);

      ViewData["Message"] = json.message;

      return View("~/Views/Test/Test.cshtml", await GetViewModel());
    }

    public async Task<IActionResult> SendEmail(Email email)
    {
      await _emailService.Insert(await GetAccessToken(), email);
      return View("Index", await GetViewModel());
    }

    public async Task<IActionResult> Claims()
    {
      var client = new RestClient("https://localhost:44350/api/test/claims");
      var request = new RestRequest(Method.GET);
      request.AddHeader("authorization", $"Bearer {await GetAccessToken()}");
      var response = client.Execute(request);

      dynamic json = JsonConvert.DeserializeObject(response.Content);

      ViewData["Message"] = response.Content;

      return View("~/Views/Test/Test.cshtml", await GetViewModel());
    }



    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region Token
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
    private Task<string> GetUserAuth0Id()
    {
      return Task.Factory.StartNew(() =>
      {
        return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
      });
    }
    #endregion

    private async Task<TestViewModel> GetViewModel()
    {
      if (User.Identity.IsAuthenticated)
      {
        return new TestViewModel(await GetAccessToken(), await GetUserAuth0Id());
      }
      else
      {
        return new TestViewModel();
      }
    }

  }
}
