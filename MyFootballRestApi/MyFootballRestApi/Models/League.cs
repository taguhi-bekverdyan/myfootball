using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class League:EntityBase<League>
    {

        public List<Team> Teams { get; set; }
        public int CountOfMatches { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        private DateTime Temp { get; set; }
        public DayOfWeek Day1 { get; set; }
        public DayOfWeek Day2 { get; set; }
        public string Per1 { get; set; }
        public string Per2 { get; set; }
        public string Per3 { get; set; }
        public List<Tour> Tour { get; set; }
        private int[,] Play;

    }
}
