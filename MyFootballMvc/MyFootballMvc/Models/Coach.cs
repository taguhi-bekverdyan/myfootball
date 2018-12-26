using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Coach : EntityBase<Coach>
    {
        public string UserId { get; set; }
        public string License { get; set; }
    }
}
