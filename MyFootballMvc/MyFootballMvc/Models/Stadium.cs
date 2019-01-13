using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Models
{
  public class Stadium
  {
    public string Owner { get; set; }
    public double StadiumLength { get; set; }
    public double StadiumWidth { get; set; }
    public Surface Surface { get; set; }
    public bool HasLigthing { get; set; }
    public AreaType AreaType { get; set; }
    public bool HasLockerRoom { get; set; }
    public bool HasFieldHeating { get; set; }
    public bool HasTribunes { get; set; }
    public double PricePerHour { get; set; }
    public WorkingHours WorkingHours { get; set; }
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
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
  }
}
