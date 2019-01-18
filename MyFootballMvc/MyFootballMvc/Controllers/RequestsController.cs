using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class RequestsController : Controller
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View("Index",await GetRequestsIndexViewModel());
        }

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