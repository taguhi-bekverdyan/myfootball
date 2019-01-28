using MyFootballMvc.Models;
using MyFootballMvc.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.ViewModels
{
    public class MyNotificationsViewModel:LayoutViewModel
    {

        private readonly RequestsService _requestsService;

        public List<Request> Requests { get; set; } = new List<Request>();



        public MyNotificationsViewModel():base()
        {

        }

        public MyNotificationsViewModel(string token,string id):base(token,id)
        {
            _requestsService = new RequestsService();
             Requests = _requestsService.GetRequestsByUserId(token, id).Result;
            
        }

    }
}
