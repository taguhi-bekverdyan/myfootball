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
    public class PlayerController : Controller
    {
        private readonly PlayersService _playersService;

        public PlayerController()
        {
            _playersService = new PlayersService();
        }

        public async Task<ActionResult> List()
        {
            PlayerViewModel viewModel = await GetViewModel();
            viewModel.ActiveMenuItem = "players";
            viewModel.Players = await _playersService.FindAll(await GetAccessToken());
            return View("List", viewModel);
        }

        [HttpGet("Players/Id/{id}")]
        public async Task<ActionResult> GetPlayerById(string id)
        {
            PlayerViewModel viewModel = await GetViewModel();
            viewModel.Player = await _playersService.FindPlayerById(await GetAccessToken(), id);
            return View("PlayerById", viewModel);
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