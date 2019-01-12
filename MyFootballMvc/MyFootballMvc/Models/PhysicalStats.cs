using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
    public class PhysicalStats
    {
        [Required]
        [Display(Name ="Height : cm")]
        public int Height { get; set; }
        [Required]
        [Display(Name = "Weight : kg")]
        public int Weight { get; set; }
        [Required]
        public Foot Foot { get; set; }
    }
}