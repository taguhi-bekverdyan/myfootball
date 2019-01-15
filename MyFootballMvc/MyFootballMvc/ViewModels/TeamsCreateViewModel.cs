using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{

  public enum ViewType
  {
    Create,
    Update
  }

  public class TeamsCreateViewModel : LayoutViewModel
  {

    public Team Team { get; set; }
    public ViewType ViewType { get; set; }

    public TeamsCreateViewModel()
    {

    }

    public TeamsCreateViewModel(string token, string id) : base(token, id)
    {

    }
  }
}
