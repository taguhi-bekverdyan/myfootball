using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
  public class Pitch : EntityBase<Pitch>
  {
    [Required]
    public string Name { get; set; }

    [Required]
    public string Owner { get; set; }
    public double? PitchLength { get; set; }
    public double? PitchWidth { get; set; }
    public Surface Surface { get; set; }
    public AreaType AreaType { get; set; }
    public bool HasLigthing { get; set; }
    public bool HasLockerRoom { get; set; }
    public bool HasFieldHeating { get; set; }
    public bool HasTribunes { get; set; }
    public double? PricePerHour { get; set; }
    public string GeoLong { get; set; }
    public string GeoLat { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
    public DateTime? StartTime { get; set; }

    [DataType(DataType.Time)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
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
