using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Card : MatchEventBase
    {
        Player Player { get; set; }
        GoalType GoalType { get; set; }
    }
    public enum CardType
    {
        Yellow,
        Red
    }
}
