using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class CupViewModel : TournamentViewModel
    {
        public CupViewModel():base()
        {

        }

        public CupViewModel(string token, string id) : base(token,id)
        {

        }
    }
}
