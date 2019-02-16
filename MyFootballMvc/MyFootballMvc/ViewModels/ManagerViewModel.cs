using Microsoft.AspNetCore.Http;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class ManagerViewModel : LayoutViewModel
    {

        public List<Coach> Managers { get; set; } = new List<Coach>();
        public Coach Manager { get; set; } = new Coach();
        public List<Team> Teams { get; set; } = new List<Team>();

        public ManagerViewModel ()
        {

        }

        public ManagerViewModel(string token, string id) : base(token, id)
        {

        }


        

    }
}
