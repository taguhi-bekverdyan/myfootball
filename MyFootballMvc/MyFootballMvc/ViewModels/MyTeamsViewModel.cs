using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyTeamsViewModel:LayoutViewModel
    {

        

        public MyTeamsViewModel():base()
        {

        }

        public MyTeamsViewModel(string token,string id):base(token,id)
        {

        }

    }
}
