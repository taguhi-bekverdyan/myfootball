using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class InviteActionParam
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public RequestTo RequestTo { get; set; }
        public string Message { get; set; }
    }
}
