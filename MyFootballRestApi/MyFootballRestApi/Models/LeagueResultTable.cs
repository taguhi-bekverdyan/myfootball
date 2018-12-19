using System.Collections.Generic;
using System.Linq;

namespace MyFootballRestApi.Models
{
    public class LeagueResultTable
    {
        public List<LeagueResultRow> Table { get; set; }

        public void Recalculate(List<Rule> rules)
        {
            rules=rules.OrderBy(x=>x.RulePriority).ToList();
            for (int i=0;i<rules.Count; i++)
            {
                Table = rules[i].Order(Table, rules.GetRange(0,i));
            }
        }

    }
}
