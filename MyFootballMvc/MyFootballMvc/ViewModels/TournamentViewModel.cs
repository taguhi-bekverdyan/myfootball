using MyFootballMvc.Models;

namespace MyFootballMvc.ViewModels
{
    public class TournamentViewModel : LayoutViewModel
    {
        public Tournament Tournament { get; set; }

        public TournamentViewModel():base()
        {

        }

        public TournamentViewModel(string token,string id):base(token,id)
        {

        }

    }
}
