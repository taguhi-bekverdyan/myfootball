using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{ 
    public class MyTeamIndexViewModel:MyTeamViewModel
    {
        

        public MyTeamIndexViewModel():base()
        {

        }

        public MyTeamIndexViewModel(string token ,string id) :base(token,id)
        {


    }

  }
}
