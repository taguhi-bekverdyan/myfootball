using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
    public class Player : EntityBase<Player>
    {
        
        
        [Required]
        public Position Position { get; set; }
        public byte[] Avatar { get; set; }
        [Required]
        public PhysicalStats PhysicalStats { get; set; }
        public bool HasTrained { get; set; }
        public string TeamId { get; set; }
        [Required]
        public PlayerStatus PlayerStatus { get; set; }

        // if has teamId
        public int Number { get; set; }

        // if has teamId
        public bool IsCaptain { get; set; }

        
        public User User { get; set; }
    }
}
