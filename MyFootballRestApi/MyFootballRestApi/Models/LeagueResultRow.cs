using System;

namespace MyFootballRestApi.Models
{
    public class LeagueResultRow
    {
        public int Position { get; set; }
        public Guid  TeamId { get; set; }
        public int Played { get; set; }
        public int Won { get; set; }
        public int Drawn { get; set; }
        public int Lost { get; set; }
        public int GF { get; set; }
        public int GA { get; set; }
        public int GD { get; set; }
        public int Point { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }

    }
}