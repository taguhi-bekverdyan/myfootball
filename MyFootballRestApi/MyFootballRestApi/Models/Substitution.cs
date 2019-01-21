using MyFootballRestApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMyFootballRestApi.Models
{
    class Substitution : MatchEventBase
    {
        Player Out {get;set;}
       Player In{get;set;}
    }
}
