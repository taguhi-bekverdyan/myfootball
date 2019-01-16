using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class TeamsIndexViewModel:LayoutViewModel
    {

        public List<Team> Teams { get; set; }

        public TeamsIndexViewModel():base()
        {

        }

        public TeamsIndexViewModel(string token ,string id,List<Team> teams):base(token,id)
        {
            Teams = teams;
        }

    }
}
