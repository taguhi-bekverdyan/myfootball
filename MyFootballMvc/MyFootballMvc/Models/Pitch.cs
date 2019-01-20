using System;
using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
  public class Pitch : EntityBase<Pitch>
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public string Owner { get; set; }

    [Display(Name = "Length")]
    public double? PitchLength { get; set; }

    [Display(Name = "Width")]
    public double? PitchWidth { get; set; }

    public Surface Surface { get; set; }

    [Display(Name = "Area")]
    public AreaType AreaType { get; set; }

    [Display(Name = "Ligthing")]
    public bool HasLigthing { get; set; }

    [Display(Name = "Locker-room")]
    public bool HasLockerRoom { get; set; }

    [Display(Name = "Field heating")]
    public bool HasFieldHeating { get; set; }

    [Display(Name = "Tribunes")]
    public bool HasTribunes { get; set; }

    [Display(Name = "Price (per hour)")]
    public double? PricePerHour { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    [Display(Name = "Start time")]
    public DateTime? StartTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    [Display(Name = "End time")]
    public DateTime? EndTime { get; set; }

    public User User { get; set; }
  }

  public enum AreaType
  {
    None,
    Indoor,
    Outdoor
  }

  public enum Surface
  {
    None,
    Ground,
    Floor,
    Grass
  }
}
