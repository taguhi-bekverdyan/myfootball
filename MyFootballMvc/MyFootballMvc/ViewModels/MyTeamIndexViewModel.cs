using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{

    public enum ViewMode
    {
        Coaches,
        Players,
        StaffMemebers,
        Fixtures,
        Description,
        SentRequests,
        Other
    }

    public class MyTeamIndexViewModel:MyTeamViewModel
    {

        private RequestsService _requestsService;

        public ViewMode ViewMode { get; set; }

        public List<SentRequestsParialArg> SentRequests { get; set; } = new List<SentRequestsParialArg>();

        public MyTeamIndexViewModel(ViewMode mode):base()
        {
            ViewMode = mode;
        }

        public MyTeamIndexViewModel(string token ,string id, ViewMode mode) :base(token,id)
        {
            ViewMode = mode;
            ChackMode(token);
        }

        private void ChackMode(string token) {
            if (ViewMode == ViewMode.SentRequests) {
                _requestsService = new RequestsService();
                List<Request> sent = _requestsService.FindRequestsByTeamId(token,Team.Id).Result;
                foreach (var item in sent)
                {
                    SentRequests.Add(new SentRequestsParialArg(item,token));
                }
            }
        }

    }
}
