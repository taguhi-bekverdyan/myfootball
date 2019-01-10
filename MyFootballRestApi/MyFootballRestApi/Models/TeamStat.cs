using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class TeamStat:EntityBase<TeamStat>
    {
        public int AllGoals { get; set; }
    }
}
