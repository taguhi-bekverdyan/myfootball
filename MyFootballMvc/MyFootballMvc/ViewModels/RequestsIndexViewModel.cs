using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System.Collections.Generic;

namespace MyFootballMvc.ViewModels
{
    public class RequestsIndexViewModel:MyTeamViewModel
    {

        public List<Player> Players { get; set; }
        public List<Staff> Staffs { get; set; }
        public List<Coach> Coaches { get; set; }

        private readonly PlayersService _playersService;
        private readonly StaffService _staffService;
        private readonly CoachService _coachService;

        public RequestsIndexViewModel()
        {
            _playersService = new PlayersService();
            _staffService = new StaffService();
            _coachService = new CoachService();
        }

        public RequestsIndexViewModel(string token, string id) : base(token,id)
        {
            _playersService = new PlayersService();
            Players = _playersService.FindFreePlayers(token,Team.Id).Result;
            _staffService = new StaffService();
            Staffs = _staffService.FindFreeStafs(token, Team.Id).Result;
            _coachService = new CoachService();
            Coaches = _coachService.FindFreeCoaches(token, Team.Id).Result;
        }

    }
}
