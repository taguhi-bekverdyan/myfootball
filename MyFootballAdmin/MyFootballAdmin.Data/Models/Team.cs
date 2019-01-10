using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Team:EntityBase<Team>
    {
        public string Name { get; set; }
        public string ShartName { get; set; }
        public User President { get; set; }
        
        public List<Staff> StaffMembers { get; set; }
        public List<Player> Players { get; set; }
        public List<Coach> Managers { get; set; }
        public List<string> Stats { get; set; }
    }
}
