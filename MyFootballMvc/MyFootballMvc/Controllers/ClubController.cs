using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class ClubController : Controller
    {
        private readonly TeamsService _teamsService;

        public ClubController()
        {
            _teamsService = new TeamsService();
        }

        public async Task<ActionResult> ClubsList()
        {
            ClubViewModel viewModel = await GetViewModel();
            viewModel.ActiveMenuItem = "clubs";
            viewModel.Clubs = await _teamsService.FindAll(await GetAccessToken());
            return View("ClubsList", viewModel);
        }

        [HttpGet("Club/Id/{id}")]
        public async Task<ActionResult> GetClubById(string id)
        {
            ClubViewModel viewModel = await GetViewModel();
            viewModel.Club = await _teamsService.FindTeamById(await GetAccessToken(), id);
            return View("ClubById", viewModel);
        }


        #region Token
        private async Task<string> GetAccessToken()
        {
            if (User.Identity.IsAuthenticated)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var accessTokenExpiresAt = DateTime.Parse(
                    await HttpContext.GetTokenAsync("expires_at"),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.RoundtripKind);
                var idToken = await HttpContext.GetTokenAsync("id_token");
                return accessToken;
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

        private async Task<ClubViewModel> GetViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new ClubViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new ClubViewModel();
            }
        }
    }
}