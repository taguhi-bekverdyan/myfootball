using System.Collections.Generic;
using System.Linq;

namespace MyFootballAdmin.Data.Models
{
    public class Rule
    {
        public string RuleName { get; set; }
        public int RulePriority { get; set; }
        public string ParameterName { get; set; }
    }
}