using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class RequestDetailsViewModel:MyTeamIndexViewModel
    {
        public RequestDetailsViewModel():base()
        {

        }

        public RequestDetailsViewModel(string token,string id):base(token,id)
        {

        }
    }
}
