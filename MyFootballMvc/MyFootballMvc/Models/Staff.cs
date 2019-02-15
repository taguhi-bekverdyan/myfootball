using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Staff : EntityBase<Staff>
    {
        [Required(ErrorMessage ="Please enter your Occupation")]
        public string Occupation { get; set; }
        [Required(ErrorMessage ="Please enter your License")]
        public string License { get; set; }
        [Required]
        public StaffStatus StaffStatus { get; set; }
        public string Avatar { get; set; }
        public string TeamId { get; set; }

        public User User { get; set; }
    }
}
