using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class TeamStat:EntityBase<TeamStat>
    {
        public int AllGoals { get; set; }

        public TeamStat()
        {
            Created = Updated = DateTime.Now;
        }
    }
}
