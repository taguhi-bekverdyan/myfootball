using System;
using System.ComponentModel.DataAnnotations;

namespace MyFootballMvc.Models
{
  public class Stadium : EntityBase<Stadium>
  {
    [Required]
    public string Owner { get; set; }

    [Display(Name = "Length")]
    public double? StadiumLength { get; set; }

    [Display(Name = "Width")]
    public double? StadiumWidth { get; set; }

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

    [Display(Name = "Working hours")]
    public WorkingHours WorkingHours { get; set; } = new WorkingHours();
  }

  public enum AreaType
  {
    Indoor,
    Outdoor
  }

  public enum Surface
  {
    Ground,
    Floor,
    Grass
  }

  public class WorkingHours
  {
    [Display(Name = "Start time")]
    public TimeSpan? StartTime { get; set; }

    [Display(Name = "End time")]
    public TimeSpan? EndTime { get; set; }
  }
}
