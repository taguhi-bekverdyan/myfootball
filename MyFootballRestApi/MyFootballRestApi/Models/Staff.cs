using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Staff:EntityBase<Staff>
    {
        public string UserId { get; set; }
        public string Occupation { get; set; }
        public string License { get; set; }
    }
}
