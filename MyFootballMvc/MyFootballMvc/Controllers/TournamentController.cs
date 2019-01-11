using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Controllers
{
    public class TournamentController : Controller
    {
        public async Task<IActionResult> Index(string tournamentId)
        {
            var client = new RestClient($@"https://localhost:44350/api/Tournament/{tournamentId}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var tournament = JsonConvert.DeserializeObject<Tournament>(response.Content);



            switch (tournament.TournamentType)
            {
                case TournamentType.League:
                    LeagueViewModel leagueViewModel = await GetLeagueViewModel(tournament.Id);
                    return View("~/Views/League/Index.cshtml", leagueViewModel);
                case TournamentType.Cup:
                    var cupViewModel = await GetCupViewModel();
                    return View("~/Views/Cup/Index.cshtml", cupViewModel);
                default:
                    var tournamentViewModel = await GetTournamentViewModel();
                    return View("~/");
            }



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

        private async Task<CupViewModel> GetCupViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new CupViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new CupViewModel();
            }
        }

        private async Task<TournamentViewModel> GetTournamentViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new TournamentViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new TournamentViewModel();
            }
        }

        private async Task<LeagueViewModel> GetLeagueViewModel(string tournamentId)
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
}