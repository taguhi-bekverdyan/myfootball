using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class TeamsController : Controller
    {
        private readonly ImageUploadService _imageUploadService;
        private readonly TeamsService _teamsService;
        private readonly UsersService _usersSevice;


        public TeamsController()
        {
            _usersSevice = new UsersService();
            _teamsService = new TeamsService();
            _imageUploadService = new ImageUploadService();
        }

        [Authorize]
        [Route("Teams/Index")]
        public async Task<IActionResult> Index()
        {
            var viewModel = await GetTeamsIndexViewModel();
            return View("Index", viewModel);
        }

        private async Task<string> UploadImageAsync(string base64String)
        {
            var imageBytes = Convert.FromBase64String(base64String);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                return await _imageUploadService.UploadImageAsync(ms, DateTime.Now.ToLongTimeString() + Guid.NewGuid());
            }
        }

        #region Validation actions

        public async Task<JsonResult> CheckName([Bind(Prefix = "Team.Name")] string name)
        {
            var result = await _teamsService.FindTeamByName(await GetAccessToken(), name);

            if (result is Team)
                return Json(false);
            return Json(true);
        }

        public async Task<JsonResult> CheckShortName([Bind(Prefix = "Team.ShortName")] string shortName,
            [Bind(Prefix = "Team.Name")] string name)
        {
            var team = await _teamsService.FindTeamByShortName(await GetAccessToken(), shortName);
            bool condition1, condition2;

            condition1 = team is Team ? false : true;
            condition2 = shortName.ToLower()[0] == name.ToLower()[0] ? true : false;

            return condition1 && condition2 ? Json(true) : Json(false);
        }

        #endregion

        #region TEAM_INFO_ACTIONS

        [Route("Teams/Fixtures")]
        public async Task<IActionResult> Fixtures()
        {
            var teamPlayersViewModel = await GetMyTeamPlayersViewModel();
            teamPlayersViewModel.ActiveMenuItem = "teamFixtures";
            return View("Fixtures", teamPlayersViewModel);
        }

        [Route("Teams/Players")]
        public async Task<IActionResult> Players()
        {
            var teamPlayersViewModel = await GetMyTeamPlayersViewModel();
            teamPlayersViewModel.ActiveMenuItem = "teamPlayers";
            return View("Players", teamPlayersViewModel);
        }

        [Route("Teams/Coaches")]
        public async Task<IActionResult> Coaches()
        {
            var teamCoachViewModel = await GetMyTeamCoachesViewModel();
            teamCoachViewModel.ActiveMenuItem = "teamCoaches";
            return View("Coaches", teamCoachViewModel);
        }

        [Route("Teams/StaffMembers")]
        public async Task<IActionResult> StaffMembers()
        {
            var teamStaffMembersViewModel = await GetMyTeamStaffMembersViewModel();
            teamStaffMembersViewModel.ActiveMenuItem = "teamStaffMembers";
            return View("StaffMembers", teamStaffMembersViewModel);
        }

        [Route("Teams/SentRequests")]
        public async Task<IActionResult> SentRequests()
        {
            var teamSentRequestsViewModel = await GetSentRequestsViewModel();
            teamSentRequestsViewModel.ActiveMenuItem = "teamSentRequests";
            return View("SentRequests", teamSentRequestsViewModel);
        }

        #endregion

        #region CREATE_TEAM_ACTIONS

        [Route("Teams/Create")]
        public async Task<IActionResult> Create()
        {
            var viewModel = await GetTeamsCreateViewModel();
            viewModel.Team = new Team();
            viewModel.Team.Avatar = await _imageUploadService.GetTeamDefaultAvatarAsync();
            viewModel.ViewType = ViewType.Create;

            return View("CreateOrUpdate", viewModel);
        }

        [HttpPost("Teams/CreateOrUpdate")]
        public async Task<IActionResult> CreateOrUpdate(Team team)
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();


            if (!ModelState.IsValid)
            {
                var viewModel = await GetTeamsCreateViewModel();
                viewModel.Team = team;
                viewModel.ViewType = ViewType.Update;
                return View("CreateOrUpdate", viewModel);
            }

            if (!team.Avatar.Contains("https://")) team.Avatar = await UploadImageAsync(team.Avatar.Split(',')[1]);

            var user = await _usersSevice.FindUserById(token, id);

            if (string.IsNullOrEmpty(team.Id))
            {
                team.President = user;
                team.SentRequests = new List<string>();
                await _teamsService.Insert(token, team);
            }
            else
            {
                var current = await _teamsService.FindTeamById(token, team.Id);
                current.Name = team.Name;
                current.ShortName = team.ShortName;
                current.Avatar = team.Avatar;
                await _teamsService.Update(token, current);
            }

            return RedirectToAction("Index");
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

        private async Task<MyTeamIndexViewModel> GetTeamsIndexViewModel()
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();
            return new MyTeamIndexViewModel(token, id);
        }

        private async Task<SentRequestsViewModel> GetSentRequestsViewModel()
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();
            return new SentRequestsViewModel(token, id);
        }

        private async Task<MyTeamPlayersViewModel> GetMyTeamPlayersViewModel()
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();
            return new MyTeamPlayersViewModel(token, id);
        }

        private async Task<MyTeamCoachesViewModel> GetMyTeamCoachesViewModel()
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();
            return new MyTeamCoachesViewModel(token, id);
        }

        private async Task<MyTeamStaffMembersViewModel> GetMyTeamStaffMembersViewModel()
        {
            var token = await GetAccessToken();
            var id = await GetUserAuth0Id();
            return new MyTeamStaffMembersViewModel(token, id);
        }

        #endregion
    }
}