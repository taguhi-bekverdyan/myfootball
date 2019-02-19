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
        public string ShortName { get; set; }
        public User President { get; set; }
        
        public List<Staff> StaffMembers { get; set; }
        public List<Player> Players { get; set; }
        public List<Coach> Managers { get; set; }
        public List<string> Stats { get; set; }

        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }

        public string Avatar { get; set; }

        public List<string> SentRequests { get; set; }

        public Team()
        {
            Name = " ";
            StaffMembers = new List<Staff>();
            Players = new List<Player>();
            Managers = new List<Coach>();
            Stats = new List<string>();

            Created = Updated = DateTime.Now;
        }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
