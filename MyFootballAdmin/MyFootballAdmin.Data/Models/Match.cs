using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Match:EntityBase<Match>
    {       
        public DateTime MatchDateTime { get; set; }
        public Team Team1 { get; set; }
        public Team Team2 { get; set; }
        public int GoalClub1 { get; set; }
        public int GoalClub2 { get; set; }
        public Guid RefereeId { get; set; }
    }
}
