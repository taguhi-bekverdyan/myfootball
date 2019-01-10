using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Models
{
    public class Staff:EntityBase<Staff>
    {
        
        public string Occupation { get; set; }
        public string License { get; set; }
        public StaffStatus StaffStatus { get; set; }
        public string TeamId { get; set; }
        public User User { get; set; }

    }
}
