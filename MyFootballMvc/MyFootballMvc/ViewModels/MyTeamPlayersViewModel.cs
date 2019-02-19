using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFootballMvc.Services;

namespace MyFootballMvc.ViewModels
{
    public class MyTeamPlayersViewModel:MyTeamViewModel
    {
        public List<Player> MyPlayers { get; set; }

        public MyTeamPlayersViewModel():base()
        {

        }

        public MyTeamPlayersViewModel(string token,string id):base(token,id)
        {
            MyPlayers = Team.Players;
        }

        

    }
}
