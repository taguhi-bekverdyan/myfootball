using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class RequestsIndexViewModel:MyTeamViewModel
    {

        public List<Player> Players { get; set; }
        //public List<Staff> Staffs { get; set; }
        //public List<Coach> Coaches { get; set; }

        private readonly PlayersService _playersService;

        public RequestsIndexViewModel()
        {
            _playersService = new PlayersService();
        }

        public RequestsIndexViewModel(string token, string id) : base(token,id)
        {
            _playersService = new PlayersService();
            Players = _playersService.FindFreePlayers(token).Result;
        }

    }
}
