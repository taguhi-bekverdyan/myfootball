﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Tour
    {
        public List<Match> Matches { get; set; }
        public bool IsPlayed { get; set; }
    }
}