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
    public class PlayerController : Controller
    {
        private readonly PlayersService _playersService;
        private readonly TeamsService _teamsService;
        private readonly LeaguesService _leagueService;

        public PlayerController()
        {
            _playersService = new PlayersService();
            _teamsService = new TeamsService();
            _leagueService = new LeaguesService();
        }

        public async Task<ActionResult> List()
        {
            PlayerViewModel viewModel = await GetViewModel();
            viewModel.ActiveMenuItem = "players";
            viewModel.Teams = await _teamsService.FindAll(await GetAccessToken());
            viewModel.Players = await _playersService.FindAll(await GetAccessToken());
            return View("List", viewModel);
        }

        [HttpGet("Player/Id/{id}")]
        public async Task<ActionResult> GetPlayerById(string id)
        {
            PlayerViewModel viewModel = await GetViewModel();
            viewModel.Player = await _playersService.FindPlayerById(await GetAccessToken(), id);
            viewModel.Teams = await _teamsService.FindAll(await GetAccessToken());
            return View("PlayerById", viewModel);
        }

        [HttpPost("Player/set_number")]
        public async Task<IActionResult> SetPlayerNumber([FromBody]SetPlayerNumberActionArg arg)
        {
            try
            {
                string token = await GetAccessToken();
                string id = await GetUserAuth0Id();

                Team team = await _teamsService.FindTeamByUserId(token,id);
                Player player = team.Players.FirstOrDefault(p => p.Id == arg.PlayerId);

                if (!(team.Players.Any(p => p.Number == arg.Number)))
                {
                    player.Number = arg.Number;
                    await _playersService.Update(token, player);
                    await _teamsService.Update(token,team);
                    List<League> leagues = await _leagueService.FindAll();

                    foreach (var league in leagues)
                    {
                        for (int i = 0; i < league.Teams.Count; i++)
                        {
                            if (league.Teams.ElementAt(i).Id == team.Id) {
                                league.Teams[i] = team;
                            }
                        }
                        await _leagueService.Update(token,league);
                    }

                   

                    return Ok(200);                   
                }
                else {
                    return StatusCode(500);
                }
                
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
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
            return Task.Factory.StartNew(() =>
            {
                return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            });
        }
        #endregion

        private async Task<PlayerViewModel> GetViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new PlayerViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new PlayerViewModel();
            }
        }
    }
}