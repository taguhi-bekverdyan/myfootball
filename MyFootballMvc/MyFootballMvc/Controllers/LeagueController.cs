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
            var league = await _leaguesService.FindLeagueByTournamentId(tournamentId);
            leagueViewModel.FixtureViewItem.IsGenerated = league.Tournament.IsGenerated;
            leagueViewModel.FixtureViewItem.Tours = league.Tour;
            leagueViewModel.ActiveMenuItem = "fixtures";
            return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Results(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "results";
            return View("~/Views/League/Results.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Tables(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "tables";
            return View("~/Views/League/Tables.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Clubs(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "leagueClubs";

            return View("~/Views/League/Clubs.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Players(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "leaguePlayers";
            var token = await GetAccessToken();
            var league = await _leaguesService.FindLeagueByTournamentId(tournamentId);
            var leagueTeams = league.Teams;
            var teams = new List<Team>();
            foreach (var leagueTeam in leagueTeams)
            {
                var team = await _teamsService.FindTeamById(token, leagueTeam.Id);
                teams.Add(team);
            }


            foreach (var team in teams)
            {
                var players = await  _teamsService.FindPlayersByTeamId(token, team.Id);
                if (players != null && players.Count != 0)
                    leagueViewModel.Players.AddRange(players);
            }

            return View("~/Views/League/Players.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Managers(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "leagueManagers";
            var league = await _leaguesService.FindLeagueByTournamentId(tournamentId);
            var token = await GetAccessToken();
            var leagueTeams = league.Teams;
            var teams = new List<Team>();
            foreach (var leagueTeam in leagueTeams)
            {

                var team = await _teamsService.FindTeamById(token, leagueTeam.Id);
                teams.Add(team);
            }

            foreach (var team in teams)
            {
                var managers = await _teamsService.FindManagersByTeamId(token, team.Id);
                if (managers != null && managers.Count != 0)
                    leagueViewModel.Managers.AddRange(managers);
            }

            return View("~/Views/League/Managers.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> News(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "news";
            return View("~/Views/League/News.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Social(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "social";
            return View("~/Views/League/Social.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> History(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "history";
            return View("~/Views/League/History.cshtml", leagueViewModel);
        }

        public async Task<IActionResult> Referees(string tournamentId)
        {
            var leagueViewModel = await GetViewModel(tournamentId);
            leagueViewModel.ActiveMenuItem = "leagueReferees";
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
    }
}