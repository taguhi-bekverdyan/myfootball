using MyFootballMvc.Interfaces;
using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyTeamCoachesViewModel:MyTeamViewModel
    {

        public List<Coach> MyCoaches { get; set; }

        public MyTeamCoachesViewModel():base()
        {

        }

        public MyTeamCoachesViewModel(string token,string id):base(token,id)
        {
            MyCoaches = Team.Managers;
        }

    }
}
