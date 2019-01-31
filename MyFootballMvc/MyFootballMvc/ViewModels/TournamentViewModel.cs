using MyFootballMvc.Models;

namespace MyFootballMvc.ViewModels
{
    public class TournamentViewModel : LayoutViewModel
    {
        public Tournament Tournament { get; set; } = new Tournament();

        public TournamentViewModel():base()
        {

        }

        public TournamentViewModel(string token,string id):base(token,id)
        {

        }

    }
}
