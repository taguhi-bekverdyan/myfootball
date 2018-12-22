using System;
using System.Collections.Generic;

namespace MyFootballRestApi.Models
{
    public class League:EntityBase<League>
    {        
        public List<Team> Teams { get; set; }
        public int CountOfMatches { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DayOfWeek Day1 { get; set; }
        public DayOfWeek Day2 { get; set; }
        public List<Rule> Rules { get; set; }
        public List<Pause> Pauses { get; set; }
        public List<Tour> Tour { get; set; }
        public LeagueResultTable ReultTable { get; set; }

    }
}
