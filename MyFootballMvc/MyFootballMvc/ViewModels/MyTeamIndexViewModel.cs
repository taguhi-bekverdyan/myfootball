using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{

  public enum ViewMode
  {
    Coaches,
    Players,
    StaffMemebers,
    Fixtures,
    Description
  }

  public class MyTeamIndexViewModel : MyTeamViewModel
  {

    public ViewMode ViewMode { get; set; }

    public MyTeamIndexViewModel(ViewMode mode) : base()
    {
      ViewMode = mode;
    }

    public MyTeamIndexViewModel(string token, string id, ViewMode mode) : base(token, id)
    {
      ViewMode = mode;
    }

  }
}
