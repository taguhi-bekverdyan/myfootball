using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class EditAccountViewModel
    {
        public User User { get; set; }
        public List<Team> Teams { get; set; }
    }
}
