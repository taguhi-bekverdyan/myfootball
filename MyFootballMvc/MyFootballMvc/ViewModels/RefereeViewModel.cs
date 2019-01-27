using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
  public class RefereeViewModel : LayoutViewModel
  {
    public List<Referee> Referees { get; set; }
    public Referee Referee { get; set; }

    public RefereeViewModel()
    {

    }

    public RefereeViewModel(string token, string id) : base(token, id)
    {

    }
  }
}
