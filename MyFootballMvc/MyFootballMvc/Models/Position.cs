
using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
    public enum Position
    {
        [Display(Name = "Goalkeeper")]
        Goalkeeper,
        [Display(Name ="Central Back")]
        CentralDefender,
        [Display(Name = "Left Back")]
        LeftBack,
        [Display(Name = "Right Back")]
        RightBack,
        [Display(Name = "Right Wing Back")]
        RightWingBack,
        [Display(Name = "Left Wing Back")]
        LeftWingBack,
        [Display(Name = "Central Midfielder")]
        CentralMidfielder,
        [Display(Name = "Central Attacking Midfielder")]
        CentralAttackingMidfielder,
        [Display(Name = "Central Defensive Midfielder")]
        CentralDefensiveMidfielder,
        [Display(Name = "Right Midfielder")]
        RightMidfielder,
        [Display(Name = "Left Midfielder")]
        LeftMidfielder,
        [Display(Name = "Left Forward")]
        LeftForward,
        [Display(Name = "Right Forward")]
        RightForward,
        [Display(Name = "Striker")]
        Striker,
        [Display(Name ="Forward")]
        Forward
    }
}