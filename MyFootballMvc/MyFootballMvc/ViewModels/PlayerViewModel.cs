using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class PlayerViewModel:LayoutViewModel
    {
        public List<Player> Players { get; set; } = new List<Player>();
        public Player Player { get; set; } = new Player();
        public List<Team> Teams { get; set; }

        public PlayerViewModel()
        {

        }

        public PlayerViewModel(string token, string id) : base(token, id)
        {

        }
    }
}
