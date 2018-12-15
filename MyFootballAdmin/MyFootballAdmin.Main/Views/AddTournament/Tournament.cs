using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class Tournament
    {
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int teamsMinCount { get; set; }
        public int teamsMaxCount { get; set; }
        public TournamentType tournamentType { get; set; }
        public ResponseMatch responseMatch { get; set; }
        public Tournament()
        {

        }
    }

    public enum TournamentType
    {
        League,
        Cup,
    }

    public enum ResponseMatch
    {
        Yes,
        No
    }

}
