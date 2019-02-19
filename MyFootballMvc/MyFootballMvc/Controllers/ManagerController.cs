using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MyFootballMvc.Controllers
{
    public class ManagerController : Controller
    {
        private readonly CoachService _coachService;
        private readonly TeamsService _teamsService;

        public ManagerController()
        {
            _coachService = new CoachService();
            _teamsService = new TeamsService();
        }

        public async Task<ActionResult> ManagersList()
        {
            ManagerViewModel viewModel = await GetViewModel();
            viewModel.ActiveMenuItem = "managers";
            viewModel.Teams = await _teamsService.FindAll(await GetAccessToken());
            //viewModel.Managers = await _teamsService.FindAllManagers();
            viewModel.Managers = await _coachService.FindAll(await GetAccessToken());
            return View("ManagersList", viewModel);
        }

        [HttpGet("Manager/Id/{id}")]
        public async Task<ActionResult> GetManagerById(string id)
        {
            ManagerViewModel viewModel = await GetViewModel();
            viewModel.Teams = await _teamsService.FindAll(await GetAccessToken());
            viewModel.Manager = await _coachService.FindCoachById(await GetAccessToken(), id);
            return View("ManagerById", viewModel);
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

        private async Task<ManagerViewModel> GetViewModel()
        {
            if (User.Identity.IsAuthenticated)
            {
                return new ManagerViewModel(await GetAccessToken(), await GetUserAuth0Id());
            }
            else
            {
                return new ManagerViewModel();
            }
        }
    }
}
