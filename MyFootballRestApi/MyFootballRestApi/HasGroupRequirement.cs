using System;
using Microsoft.AspNetCore.Authorization;

namespace MyFootballRestApi
{
    public class HasGroupRequirement : IAuthorizationRequirement
    {
        public string Issuer { get; }
        public string Group { get; }

        public HasGroupRequirement(string group, string issuer)
        {
            Group = group ?? throw new ArgumentNullException(nameof(group));
            Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
        }
    }
}
