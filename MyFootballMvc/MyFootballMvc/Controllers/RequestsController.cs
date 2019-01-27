using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class RequestsController : Controller
    {

        private readonly PlayersService _playersService;
        private readonly TeamsService _teamsService;
        private readonly StaffService _staffService;
        private readonly CoachService _coachService;
        private readonly RequestsService _requestsService;

        public RequestsController()
        {
            _playersService = new PlayersService();
            _teamsService = new TeamsService();
            _staffService = new StaffService();
            _coachService = new CoachService();
            _requestsService = new RequestsService();
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index",await GetRequestsIndexViewModel());
        }

        #region POST

        [HttpPost("Requests/Invite")]
        public async Task<IActionResult> Invite([FromBody]InviteActionParam param) {
            try
            {

                string token = await GetAccessToken();
                string id = await GetUserAuth0Id();

                Team team = await _teamsService.FindTeamByUserId(token, id);

                Request request = new Request();
                request.RequestTo = param.RequestTo;
                request.UserId = await GetInvitedUserId(param.Id,param.RequestTo,token);
                request.RequestStatus = RequestStatus.InProgress;
                request.Team = team;
                request.Message = param.Message;

                await _requestsService.Insert(token,request);
                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        

        #endregion

        #region HELPERS

        private async Task<string> GetInvitedUserId(string id,RequestTo requestTo,string token)
        {
            string invitedUserId;
            switch (requestTo)
            {
                case RequestTo.Player:
                    Player player = await _playersService.FindPlayerById(token,id);
                    invitedUserId = player.User.Id;
                    break;
                case RequestTo.Staff:
                    Staff staff = await _staffService.FindStaffById(token,id);
                    invitedUserId = staff.User.Id;
                    break;
                case RequestTo.Coach:
                    Coach coach = await _coachService.FindCoachById(token,id);
                    invitedUserId = coach.User.Id;
                    break;
                default:
                    invitedUserId = null;
                    break;
            }
            return invitedUserId;
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

        private async Task<RequestsIndexViewModel> GetRequestsIndexViewModel()
        {
            return new RequestsIndexViewModel(await GetAccessToken(), await GetUserAuth0Id());
        }

        //private async Task<MyTeamIndexViewModel> GetTeamsIndexViewModel(ViewMode mode)
        //{
        //    string token = await GetAccessToken();
        //    string id = await GetUserAuth0Id();
        //    return new MyTeamIndexViewModel(token, id, mode);
        //}

        #endregion

    }
}