using System;
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
    public class LeagueController : Controller
    {
        public LeagueController()
        {
            _teamsService = new TeamsService();
            _leaguesService = new LeaguesService();
        }

        private TeamsService _teamsService { get; }
        private LeaguesService _leaguesService { get; }

        public async Task<IActionResult> Index(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Index.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Fixtures(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Results(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Results.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Tables(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Tables.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Clubs(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Clubs.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Players(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Players.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Managers(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Managers.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> News(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/News.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Social(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Social.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> History(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/History.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Referees(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Referees.cshtml", leagueViewModel);
        }


        public async Task<IActionResult> Join(string tournamentId)
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();

            var leagueViewModel = await GetViewModel(tournamentId);
            var team = await _teamsService.FindTeamByUserId(token, id);
            leagueViewModel.League.Teams.Add(team);
            await _leaguesService.Update(token, leagueViewModel.League);

            return View("~/Views/League/Index.cshtml", leagueViewModel);
        }

        private async Task<LeagueViewModel> GetViewModel(string tournamentId)
        {
            if (User.Identity.IsAuthenticated)
                return new LeagueViewModel(tournamentId, await GetAccessToken(), await GetUserAuth0Id());
            return new LeagueViewModel(tournamentId);
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
    }
}