using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class FixtureViewItem
    {
        public List<Tour> Tours { get; set; }
        public bool IsGenerated { get; set; }
    }
}
