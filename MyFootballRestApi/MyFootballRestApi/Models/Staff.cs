using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Staff:EntityBase<Staff>,IEquatable<Staff>
    {
        
        public string Occupation { get; set; }
        public string License { get; set; }
        [Required]
        public StaffStatus StaffStatus { get; set; }

        public string TeamId { get; set; }

        [Required]
        public User User { get; set; }

        public bool Equals(Staff other)
        {
            return Id == other.Id;
        }
    }
}
