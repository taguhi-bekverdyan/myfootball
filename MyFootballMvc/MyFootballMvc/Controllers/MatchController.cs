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
    public class MatchController : Controller
    {
        private readonly MatchService _matchService;
        private readonly RefereeService _refereeService;

        public MatchController()
        {
            _matchService = new MatchService();
            _refereeService = new RefereeService();
        }

      

        [HttpGet("Match/Id/{id}")]
        public async Task<ActionResult> GetMatchById(string id)
        {
            MatchViewModel viewModel = await GetViewModel();
            viewModel.Match = await _matchService.FindMatchById(await GetAccessToken(), id);
            viewModel.Referee = await _refereeService.FindRefereeById(await GetAccessToken(), id);
            return View("MatchById", viewModel);
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

        private async Task<MatchViewModel> GetViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new MatchViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new MatchViewModel();
            }
        }
    }
}