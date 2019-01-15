using MyFootballMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
  public class StadiumViewModel : LayoutViewModel
  {
    public User User { get; set; }
    public Stadium Stadium { get; set; }

    public StadiumViewModel()
    {

    }

    public StadiumViewModel(string token, string id) : base(token, id)
    {

    }
  }
}
