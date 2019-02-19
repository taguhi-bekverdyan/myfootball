using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
  public class PitchViewModel : LayoutViewModel
  {
    public User User { get; set; }
    public Pitch Pitch { get; set; }
    public List<Pitch> MyPitches { get; set; }
    public List<Pitch> AllPitches { get; set; }

    private List<Marker> _markers;
    public List<Marker> Markers
    {
      get
      {
        _markers = new List<Marker>();

        if (AllPitches != null)
        {
          AllPitches.ForEach(pitch =>
          {
            _markers.Add(new Marker
            {
              Title = pitch.Name,
              Url = $"/Pitch/Id/{pitch.Id}",
              Lat = pitch.GeoLat,
              Lng = pitch.GeoLong
            });
          });
        }

        return _markers;
      }
    }

    public PitchViewModel()
    {

    }

    public PitchViewModel(string token, string id) : base(token, id)
    {

    }
  }

  public class Marker
  {
    public string Title { get; set; }
    public string Url { get; set; }
    public string Lng { get; set; }
    public string Lat { get; set; }
  }
}
