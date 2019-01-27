using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class ClubViewModel:LayoutViewModel
    {
        public List<Team> Clubs { get; set; }
        public Team Club { get; set; }

        public ClubViewModel(string token, string id) : base(token, id)
        {

        }

        public ClubViewModel()
        {

        }

    }
}
