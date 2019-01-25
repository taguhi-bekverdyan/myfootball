using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class League : EntityBase<League>
    {
        public List<Team> Teams { get; set; }
        public int CountOfMatches { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<DayOfWeek> MatchDays { get; set; }
        public List<Rule> Rules { get; set; }
        public List<Pause> Pauses { get; set; }
        public Tournament Tournament { get; set; }
        public List<Tour> Tour { get; set; }
        public LeagueResultTable ResultTable { get; set; }
        public int OneHalfTime { get; set; }
        public int BreakTime { get; set; }
        public int PlayersCount { get; set; }
        public int YelloCardsToDisqualification { get; set; }
    }
}
