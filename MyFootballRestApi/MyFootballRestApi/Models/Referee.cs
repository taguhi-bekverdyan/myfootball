using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Referee:EntityBase<Referee>
    {
        public string Name { get; set; }
    }
}
