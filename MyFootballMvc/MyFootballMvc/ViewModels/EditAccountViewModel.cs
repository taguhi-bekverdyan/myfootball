using MyFootballMvc.Models;
using System.Collections.Generic;

namespace MyFootballMvc.ViewModels
{
    public class EditAccountViewModel : LayoutViewModel
    {      
        public User User { get; set; }
        public List<Team> Teams { get; set; }
    }
}
