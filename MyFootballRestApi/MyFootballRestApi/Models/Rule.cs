using System.Collections.Generic;
using System.Linq;

namespace MyFootballRestApi.Models
{
    public class Rule
    {
        public string RuleName { get; set; }
        public int RulePriority { get; set; }
        public string ParameterName { get; set; }

        //public List<LeagueResultRow> Order(List<LeagueResultRow> resultTable, List<Rule> previousRules)
        //{
        //    if (previousRules.Count != 0)
        //    {
        //        foreach (var previousRule in previousRules)
        //        {
        //            var similar = GetSimalarRowItems(resultTable, previousRule);
        //            if (similar.Count == 0)
        //            { break; }
        //            else
        //            {
        //                // reflection getpropertybyname in linq

        //                //similar.OrderBy(x=>x.previouseRules.Last().ParameterName);                 
        //            }

        //        }
                
        //    }
        //    else
        //    {
        //        return resultTable.OrderBy(x => x.TeamId).ToList(); //sort by param name
        //    }
        //}

        //private List<LeagueResultRow> GetSimalarRowItems(List<LeagueResultRow> resultTable, Rule previousRule)
        //{
        //    var similatResultRowsList = new List<LeagueResultRow>();
        //    foreach (var result in resultTable)
        //    {
        //        //todo find all equal rows by parameter name property
        //        //add to similar list
        //    }
        //    return similatResultRowsList;
        //}

    }
}