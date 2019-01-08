﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class AccountController : Controller
    {

        private UsersService _usersService { get; set; }
        private TeamsService _teamsService { get; set; }
        private StaffService _staffService { get; set; }
        private PlayersService _playerService { get; set; }
        private RefereeService _refereeService { get; set; }
        private CoachService _coachService { get; set; }

        public AccountController()
        {
            _usersService = new UsersService();
            _teamsService = new TeamsService();
            _staffService = new StaffService();
            _playerService = new PlayersService();
            _refereeService = new RefereeService();
            _coachService = new CoachService();
        }

        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
            
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the 
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public async Task<IActionResult> Edit()
        {
            string accessToken = await GetAccessToken();
            string id = GetUserAuth0Id();
            List<Team> teams = await _teamsService.FindAll(accessToken);
            User user = await _usersService.FindUserById(accessToken, id);

            if (user == null)
            {
                User u = new User();
                u.Id = string.Empty;

                return View(new EditAccountViewModel()
                {
                    User = u,
                    Teams = teams,
                    IsMember = false
                });
            }

            Player player = await _playerService.GetPlayerByUserId(accessToken, id);
            Coach coach = await _coachService.GetCoachByUserId(accessToken,id);
            Staff staff = await _staffService.GetStaffByUserId(accessToken,id);
            Referee referee = await _refereeService.GetRefereeByUserId(accessToken,id);

            return View(new EditAccountViewModel()
            {
                User = user,
                Coach = coach,
                Referee = referee,
                Staff = staff,
                Player = player,
                Teams = teams,
                IsMember = true
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrUpdate(User user)
        {
            string accessToken = await GetAccessToken();
            if (!ModelState.IsValid)
            {
                return View("Edit",new EditAccountViewModel() {
                    User = user,
                    Teams = await _teamsService.FindAll(accessToken)
                });
            }


            string id = GetUserAuth0Id();
            try
            {               
                if (string.IsNullOrEmpty(user.Id))
                {
                    user.Id = id;
                    await _usersService.Insert(accessToken, user);
                }
                else
                {
                    await _usersService.Update(accessToken,user);
                    
                }
                return RedirectToAction("Edit", "Account");
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }           
        }


        #region POST

        [HttpPost("Account/Player")]
        public async Task<IActionResult> Player([FromBody] Player player)
        {


            if (!ModelState.IsValid)
            {
                //return View("Edit", new EditAccountViewModel()
                //{
                //    User = user,
                //    Teams = await _teamsService.FindAll(accessToken)
                //});
                return StatusCode(500);
            }

            string token = await GetAccessToken();
            string userId = GetUserAuth0Id();
            try
            {
                User user = await _usersService.FindUserById(token, userId);
                Player pl = await _playerService.GetPlayerByUserId(token, userId);
                player.User = user;
                if (pl == null)
                {
                    await _playerService.Insert(token, player);
                }
                else
                {
                    await _playerService.Update(token, player);
                }

                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Account/Coach")]
        public async Task<IActionResult> Coach([FromBody]Coach coach)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("the Model is invalid");
                }

                string token = await GetAccessToken();
                string userId = GetUserAuth0Id();

                User user = await _usersService.FindUserById(token, userId);

                coach.User = user;

                if (await _coachService.GetCoachByUserId(token, userId) == null)
                {
                    await _coachService.Insert(token, coach);
                }
                else
                {
                    await _coachService.Update(token,coach);
                }

                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }
        }

        [HttpPost("Account/Staff")]
        public async Task<IActionResult> Staff([FromBody]Staff staff)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("the Model is invalid");
                }

                string token = await GetAccessToken();
                string userId = GetUserAuth0Id();

                User user = await _usersService.FindUserById(token, userId);

                staff.User = user;

                if (await _staffService.GetStaffByUserId(token, userId) == null)
                {
                    await _staffService.Insert(token, staff);
                }
                else
                {
                    await _staffService.Update(token, staff);
                }

                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("Account/Referee")]
        public async Task<IActionResult> Referee([FromBody]Referee referee)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    throw new Exception("the Model is invalid");
                }

                string token = await GetAccessToken();
                string userId = GetUserAuth0Id();

                User user = await _usersService.FindUserById(token, userId);

                referee.User = user;

                if (await _refereeService.GetRefereeByUserId(token, userId) == null)
                {
                    await _refereeService.Insert(token, referee);
                }
                else
                {
                    await _refereeService.Update(token, referee);
                }

                return Ok(200);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        #endregion

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
        private string GetUserAuth0Id()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        }
        #endregion


    }
}