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
    public List<Pitch> Pitches { get; set; }

    public PitchViewModel()
    {

    }

    public PitchViewModel(string token, string id) : base(token, id)
    {

    }
  }
}
