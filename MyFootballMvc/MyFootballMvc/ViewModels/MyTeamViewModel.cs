using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyTeamViewModel:LayoutViewModel
    {

        public Team Team { get; set; }

        public MyTeamViewModel()
        {

        }

        public MyTeamViewModel(string token,string id):base(token,id)
        {
            Team = _teamsService.FindTeamByUserId(token,id).Result;
        }

    }
}
