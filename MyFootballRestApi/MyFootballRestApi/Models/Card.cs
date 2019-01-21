
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
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
