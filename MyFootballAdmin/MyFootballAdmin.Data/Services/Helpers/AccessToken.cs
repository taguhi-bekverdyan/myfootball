using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.Helpers
{
    public static class AccessToken
    {
        public static string Token { get; set; }
        public static DateTime ExpiresAt { get; set; }
    }
}
