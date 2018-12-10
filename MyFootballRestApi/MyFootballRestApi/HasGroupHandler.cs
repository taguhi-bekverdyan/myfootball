using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace MyFootballRestApi
{
    public class HasGroupHandler : AuthorizationHandler<HasGroupRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasGroupRequirement requirement)
        {
            // If user does not have the groups claim, get out of here
            if (!context.User.HasClaim(c => c.Type == "https://myfootball.am/user_authorization" && c.Issuer == requirement.Issuer))
                return Task.CompletedTask;

            var authorizationClaimString = context.User.FindFirst(c => c.Type == "https://myfootball.am/user_authorization" && c.Issuer == requirement.Issuer).Value;

            var authorizationClaim = JsonConvert.DeserializeObject<AuthorizationClaim>(authorizationClaimString);

            // get groups as array
            var groups = authorizationClaim.Groups.Split(' ');

            // Succeed if the group array contains the required scope
            if (groups.Any(s => s.ToString() == requirement.Group))
                context.Succeed(requirement);

            return Task.CompletedTask;
        }

    }



    public class AuthorizationClaim
    {
        public string Groups { get; set; }
        public string Roles { get; set; }
        public string Permissions { get; set; }
    }
}
