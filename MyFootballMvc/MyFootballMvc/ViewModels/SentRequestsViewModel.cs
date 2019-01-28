using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class SentRequestsViewModel:MyTeamViewModel
    {

        private readonly RequestsService _requestsService;

        public List<SentRequestsParialArg> SentRequests { get; set; } = new List<SentRequestsParialArg>();

        public SentRequestsViewModel():base()
        {

        }

        public SentRequestsViewModel(string token,string id):base(token,id)
        {
            _requestsService = new RequestsService();
            List<Request> sent = _requestsService.FindRequestsByTeamId(token, Team.Id).Result;
            foreach (var item in sent)
            {
                SentRequests.Add(new SentRequestsParialArg(item, token));
            }
        }

    }
}
