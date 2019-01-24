using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyRequestsViewModel:LayoutViewModel
    {

        public MyRequestsViewModel():base()
        {

        }

        public MyRequestsViewModel(string token,string id):base(token,id)
        {

        }

    }
}
