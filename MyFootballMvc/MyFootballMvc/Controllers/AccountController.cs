using System;
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

        public UsersService _usersService { get; set; }
        public TeamsService _temsService { get; set; }
        public AccountController()
        {
            _usersService = new UsersService();
            _temsService = new TeamsService();
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
            List<Team> teams = await _temsService.FindAll(accessToken);
            User user = await _usersService.FindUserById(accessToken, id);

            if (user == null)
            {
                User u = new User();
                u.Id = string.Empty;

                return View(new EditAccountViewModel()
                {
                    User = u,
                    Teams = teams                
                });
            }

            return View(new EditAccountViewModel()
            {
                User = user,
                Teams = teams
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
                    Teams = await _temsService.FindAll(accessToken)
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
                return RedirectToAction("Index","Home");
            }
            catch (Exception e)
            {
                return StatusCode(500,e);
            }           
        }

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

    }
}