using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Email: EntityBase<Email>
    {
        [Required]
        public string Address { get; set; }
        
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
