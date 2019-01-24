using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Team:EntityBase<Team>
    {
        [Required]
        [Display(Name="Team full name")]
        public string Name { get; set; }
        [Required]
        [Display(Name="Team short name")]
        public string ShortName { get; set; }
        
        public User President { get; set; }

        public List<Staff> StaffMembers { get; set; }
        public List<Player> Players { get; set; }
        public List<Coach> Managers { get; set; }
        public List<string> Stats { get; set; }

        public List<String> SentRequestsId { get; set; }

    }
}
