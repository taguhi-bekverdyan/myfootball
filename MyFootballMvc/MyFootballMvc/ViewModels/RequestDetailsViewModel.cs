using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class RequestDetailsViewModel:MyTeamIndexViewModel
    {
        public RequestDetailsViewModel(ViewMode mode):base(mode)
        {

        }

        public RequestDetailsViewModel(string token,string id,ViewMode mode):base(token,id,mode)
        {

        }
    }
}
