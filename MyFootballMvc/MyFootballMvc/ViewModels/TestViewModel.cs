using MyFootballMvc.Models;

namespace MyFootballMvc.ViewModels
{
  public class TestViewModel : LayoutViewModel
  {
    public Email Email { get; set; }

    public TestViewModel()
    {

    }

    public TestViewModel(string token, string id) : base(token, id)
    {

    }
  }
}
