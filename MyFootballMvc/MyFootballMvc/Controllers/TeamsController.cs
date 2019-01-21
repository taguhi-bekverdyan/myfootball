using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using MyFootballMvc.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MyFootballMvc.Controllers
{
  public class TeamsController : Controller
  {
    private readonly UsersService _usersSevice;
    private readonly TeamsService _teamsService;

    public TeamsController()
    {
      _usersSevice = new UsersService();
      _teamsService = new TeamsService();
    }

        [Authorize]
        [Route("Teams/Index")]
        public async Task<IActionResult> Index()
        {
            var viewModel = await GetTeamsIndexViewModel(ViewMode.Description);
            return View("Index", viewModel);
        }

        #region TEAM_INFO_ACTIONS

        [Route("Teams/Fixtures")]
        public async Task<IActionResult> Fixtures()
        {
            return View("Index", await GetTeamsIndexViewModel(ViewMode.Fixtures));
        }

        [Route("Teams/Players")]
        public async Task<IActionResult> Players()
        {
            return View("Index", await GetTeamsIndexViewModel(ViewMode.Players));
        }

        [Route("Teams/StaffMembers")]
        public async Task<IActionResult> StaffMembers()
        {
            return View("Index", await GetTeamsIndexViewModel(ViewMode.StaffMemebers));
        }

        [Route("Teams/Coaches")]
        public async Task<IActionResult> Coaches()
        {
            return View("Index", await GetTeamsIndexViewModel(ViewMode.Coaches));
        }

        [Route("Teams/Invite")]
        public async Task<IActionResult> Invite()
        {
            return View("Index", await GetTeamsIndexViewModel(ViewMode.Description));
        }

        #endregion



        #region CREATE_TEAM_ACTIONS

        [Route("Teams/Create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = await GetTeamsCreateViewModel();
            viewModel.Team = new Team();
            viewModel.ViewType = ViewType.Create;

      return View("CreateOrUpdate", viewModel);
    }

    [HttpPost("Teams/CreateOrUpdate")]
    public async Task<IActionResult> CreateOrUpdate(Team team)
    {

      string token = await GetAccessToken();
      string id = await GetUserAuth0Id();

            if (!ModelState.IsValid)
            {
                TeamsCreateViewModel viewModel = await GetTeamsCreateViewModel();
                viewModel.Team = team;
                viewModel.ViewType = ViewType.Update;
                return View("CreateOrUpdate", viewModel);
            }

            User user = await _usersSevice.FindUserById(token, id);

            if (string.IsNullOrEmpty(team.Id))
            {
                team.President = user;
                await _teamsService.Insert(token, team);
            }
            else
            {
                Team current = await _teamsService.FindTeamById(token, team.Id);
                current.Name = team.Name;
                current.ShortName = team.ShortName;
                await _teamsService.Update(token, current);
            }

            return RedirectToAction("Index", "Teams/Index");
        }

        #endregion

        #region TOKEN
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

        #region GET_METHODS_FOR_VIEW_MODELS

        private async Task<TeamsCreateViewModel> GetTeamsCreateViewModel()
        {
            return new TeamsCreateViewModel(await GetAccessToken(), await GetUserAuth0Id());
        }

        private async Task<MyTeamIndexViewModel> GetTeamsIndexViewModel(ViewMode mode)
        {
            string token = await GetAccessToken();
            string id = await GetUserAuth0Id();
            return new MyTeamIndexViewModel(token, id,mode);
        }

        #endregion

  }

}