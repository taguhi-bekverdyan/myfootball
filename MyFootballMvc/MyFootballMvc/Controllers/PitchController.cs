using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
  public class PitchController : Controller
  {
    private readonly UsersService _usersService;
    private readonly PitchService _pitchService;

    public PitchController()
    {
      _usersService = new UsersService();
      _pitchService = new PitchService();
    }

    [Route("Pitch/Index")]
    public async Task<IActionResult> Index()
    {
      return View("Index", await GetViewModel());
    }

    [HttpGet("Pitch/Create")]
    public async Task<IActionResult> Create()
    {
      return View("Create", await GetViewModel());
    }

    [HttpPost("Pitch/Create")]
    public async Task<IActionResult> Create(Pitch pitch)
    {
      string token = await GetAccessToken();
      string id = await GetUserAuth0Id();

      if (!ModelState.IsValid)
      {
        PitchViewModel viewModel = await GetViewModel();
        viewModel.Pitch = pitch;
        //viewModel.ViewType = ViewType.Update;
        return View("Create", viewModel);
      }

      User user = await _usersService.FindUserById(token, id);

      if (string.IsNullOrEmpty(pitch.Id))
      {
        pitch.User = user;
        await _pitchService.Insert(token, pitch);
      }
      else
      {
        Pitch current = await _pitchService.FindPitchById(token, pitch.Id);
        await _pitchService.Update(token, current);
      }

      return RedirectToAction("Create");
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

    private async Task<PitchViewModel> GetViewModel()
    {
      if (User.Identity.IsAuthenticated)
      {
        return new PitchViewModel(await GetAccessToken(), await GetUserAuth0Id());
      }
      else
      {
        return new PitchViewModel();
      }
    }
  }
}