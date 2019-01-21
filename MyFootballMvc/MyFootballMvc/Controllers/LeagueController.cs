using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.ViewModels;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFootballMvc.Controllers
{
  public class LeagueController : Controller
  {
    public IActionResult Index(string tournamentId)
    {
        public async Task<IActionResult> Index(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);           
            return View("~/Views/League/Index.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Fixtures(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
        }
        public async Task<IActionResult> Results(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Results.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Tables(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Tables.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Clubs(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Clubs.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Players(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Players.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Managers(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Managers.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> News(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/News.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Social(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Social.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> History(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/History.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Referees(string tournamentId)
        {
            LeagueViewModel leagueViewModel = await GetViewModel(tournamentId);
            return View("~/Views/League/Referees.cshtml", leagueViewModel);
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
            return Task.Factory.StartNew(() => {
                return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            });
        }
        #endregion

        private async Task<LeagueViewModel> GetViewModel(string tournamentId)
        {
            if (User.Identity.IsAuthenticated)
            {
                return new LeagueViewModel(tournamentId, await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new LeagueViewModel(tournamentId);
            }
        }

    }

    public IActionResult Referees(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveMenuItem = "referees";
      return View("~/Views/League/Referees.cshtml", leagueViewModel);
    }
  }
}