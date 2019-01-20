using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Tour
    {
        public List<Match> Matches { get; set; }
        public bool IsPlayed { get; set; }
         public Tour()
        {
            Matches = new List<Match>();
        }
        public override string ToString()
        {
            foreach (Match m in Matches)
            {
                Console.WriteLine(m.ToString());
            }
            return "  ";
        }
    }
}
