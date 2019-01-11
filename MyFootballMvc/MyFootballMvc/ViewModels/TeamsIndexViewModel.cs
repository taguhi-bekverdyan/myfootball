using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class TeamsIndexViewModel:LayoutViewModel
    {
        public TeamsIndexViewModel():base()
        {

        }

        public TeamsIndexViewModel(string token ,string id):base(token,id)
        {

        }

    }
}
