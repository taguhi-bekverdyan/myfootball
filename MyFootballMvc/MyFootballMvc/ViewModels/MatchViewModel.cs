using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MatchViewModel:LayoutViewModel
    {
        public Match Match { get; set; } = new Match();
        public Referee Referee { get; set; } = new Referee();
        public League League { get; set; } = new League();

        public MatchViewModel(string token, string id) : base(token, id)
        {

        }

        public MatchViewModel()
        {

        }
    }
}
