using MyFootballAdmin.Main.Views.Pauses;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class Tournament:BindableBase
    {
        public string name { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public int teamsCount { get; set; }
        public TournamentType tournamentType { get; set; }
        public int rounds { get; set; }
        public byte[] image { get; set; }
        public List<Pause> pauses { get; set; } 


        public Tournament()
        {

        }
    }

    public enum TournamentType
    {
        League,
        Cup,
    }


}
