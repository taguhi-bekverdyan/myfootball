using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
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
    private readonly LandlordService _landlordService;

    public PitchController()
    {
      _usersService = new UsersService();
      _pitchService = new PitchService();
      _landlordService = new LandlordService();
    }

    [Route("Pitch/MyPitches")]
    public async Task<IActionResult> MyPitches()
    {
      PitchViewModel pitchViewModel = await GetViewModel();
      pitchViewModel.MyPitches = await _pitchService.FindPitchesByUserId(await GetAccessToken(), await GetUserAuth0Id());
      return View("MyPitches", pitchViewModel);
    }

    [HttpGet("Pitch/Create")]
    public async Task<IActionResult> Create()
    {
      PitchViewModel viewModel = await GetViewModel();
      Landlord landlord = await _landlordService.GetLandlordByUserId(await GetAccessToken(), await GetUserAuth0Id());
      viewModel.Pitch = landlord != null ? new Pitch { Owner = landlord.Organization } : new Pitch();
      return View("Create", viewModel);
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
        return View("Create", viewModel);
      }

      User user = await _usersService.FindUserById(token, id);
      Pitch current = new Pitch();

      if (string.IsNullOrEmpty(pitch.Id))
      {
        pitch.User = user;
        current = await _pitchService.Insert(token, pitch);
      }
      else
      {
        current = await _pitchService.FindPitchById(token, pitch.Id);
        await _pitchService.Update(token, current);
      }
      
      return RedirectToAction("Id", new { id = current.Id});
    }


    public async Task<IActionResult> PitchFinder()
    {
      PitchViewModel pitchViewModel = await GetViewModel();
      pitchViewModel.AllPitches = await _pitchService.FindAll(await GetAccessToken());
      pitchViewModel.ActiveMenuItem = "pitchfinder";
      ViewBag.ApiKey = await GetAccessToken();
      return View("PitchFinder", pitchViewModel);
    }

    [HttpGet("Pitch/Id/{id}")]  
    public async Task<ActionResult> GetPitchById(string id)
    {
      PitchViewModel pitchViewModel = await GetViewModel();
      pitchViewModel.Pitch = await _pitchService.FindPitchById(await GetAccessToken(), id);
      return View("Pitch", pitchViewModel);
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