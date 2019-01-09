using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
    public enum Position
    {
        Goalkeeper,
        [Display(Name = "Central Defender")]
        CentralDefender,
        [Display(Name = "Central Defender")]
        RightWingBack,
        [Display(Name = "Left Wing Back")]
        LeftWingBack,
        [Display(Name = "Central Midfielder")]
        CentralMidfielder,
        [Display(Name = "Right Midfielder")]
        RightMidfielder,
        [Display(Name = "Left Midfielder")]
        LeftMidfielder,
        [Display(Name ="Forward")]
        Forward
    }
}