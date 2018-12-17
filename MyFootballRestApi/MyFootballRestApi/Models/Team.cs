using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class Team
    {
        [Required]
        public Guid Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(3)]
        public string Code { get; set; }
    }
}
