using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Coach:EntityBase<Coach>,IEquatable<Coach>
    {
        
        public string License { get; set; }
        [Required]
        public CoachStatus CoachStatus { get; set; }
        public string TeamId { get; set; }

        [Required]
        public User User { get; set; }

        public bool Equals(Coach other)
        {
            return Id == other.Id;
        }
    }
}
