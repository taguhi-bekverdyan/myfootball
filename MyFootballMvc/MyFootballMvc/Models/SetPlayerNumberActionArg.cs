using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class SetPlayerNumberActionArg
    {
        [Required]
        public string PlayerId { get; set; }
        [Required]
        [Range(0,100)]
        public int? Number { get; set; }
    }
}
