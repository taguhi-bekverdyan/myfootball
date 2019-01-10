using System;
using System.ComponentModel.DataAnnotations;

namespace MyFootballAdmin.Data.Models
{
    public class Player : EntityBase<Player>
    {
        
        public Position Position { get; set; }
        public byte[] Avatar { get; set; }
        public PhysicalStats PhysicalStats { get; set; }
        public bool HasTrained { get; set; }
        public string TeamId { get; set; }
        public PlayerStatus PlayerStatus { get; set; }
        public int Number { get; set; }

        // if has teamId
        public bool IsCaptain { get; set; }

        public User User { get; set; }
    }
}
