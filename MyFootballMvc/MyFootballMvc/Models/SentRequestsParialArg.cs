using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class SentRequestsParialArg
    {
        public Request Request { get; set; }
        public User User { get; set; }

        private readonly UsersService _usersService;

        public SentRequestsParialArg(Request request,string token)
        {

            _usersService = new UsersService();

            Request = request;

            User = _usersService.FindUserById(token,request.UserId).Result;

        }

    }
}
