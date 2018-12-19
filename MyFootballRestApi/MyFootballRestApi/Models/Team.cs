using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Team:EntityBase<Team>
    {
        public string Name { get; set; }
        public int Played { get; set; }
        public int Points { get; set; }
        public int Won { get; set; }
        public int Lost { get; set; }
        public int Drawn { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int GD { get; set; }

        public int LeagueId { get; set; }
    }
}
