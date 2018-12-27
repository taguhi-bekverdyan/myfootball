using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
    public class PhysicalStats
    {
        [Required]
        public int Height { get; set; }
        [Required]
        public int Weight { get; set; }
        [Required]
        public Foot Foot { get; set; }
    }
}