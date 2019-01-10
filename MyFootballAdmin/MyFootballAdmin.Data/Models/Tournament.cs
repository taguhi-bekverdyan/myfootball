using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Tournament : EntityBase<Tournament>
    {
        public string Name { get; set; }
        public int Priority { get; set; }
        public TournamentType TournamentType { get; set; }
    }

    public enum TournamentType
    {
        League,
        Cup
    }
}
