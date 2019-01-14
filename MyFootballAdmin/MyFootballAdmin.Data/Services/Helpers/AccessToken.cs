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
        public static DateTime expiresAt;
        public static DateTime ExpiresAt
        {
            set
            {
                if (expiresAt < DateTime.Now) 
                    expiresAt = value;
                else
                    System.Windows.Application.Current.Shutdown();
            }
            get { return expiresAt; }
        }
    }
}
