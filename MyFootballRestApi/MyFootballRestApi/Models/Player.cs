using System;
using System.ComponentModel.DataAnnotations;

namespace MyFootballRestApi.Models
{
    public class Player : EntityBase<Player>
    {
        
        public Position Position { get; set; }
        public byte[] Avatar { get; set; }
        public PhysicalStats PhysicalStats { get; set; }
        public bool HasTrained { get; set; }
        public string TeamId { get; set; }
        [Required]
        public PlayerStatus PlayerStatus { get; set; }

        // if has teamId
        public int Number { get; set; }

        // if has teamId
        public bool IsCaptain { get; set; }

        [Required]
        public User User { get; set; }
    }
}
