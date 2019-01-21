using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Goal : MatchEventBase
    {
        Player GoalScored { get; set; }
        Player Asistent { get; set; }
        GoalType GoalType { get; set; }
    }
    public enum GoalType
    {
        Penalty,
        OwnGoal,
        Free_Kick_Goal
    }
}
