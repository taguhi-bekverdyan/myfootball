
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.LeagueService;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.LeagueService
{
    public class LeagueService : ILeagueService
    {
        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public LeagueService()
        {
            _client = new RestClient(EndPoint);
        }

        public Task Delete(Guid id)
        {
            return Task.Factory.StartNew(() => {
                RestRequest request = new RestRequest("Leagues/{id}", Method.DELETE);
                request.AddUrlSegment("guid", id.ToString());
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }


        public Task<List<League>> FindAll()
        {
            return Task<List<League>>.Factory.StartNew(()=> {
                RestRequest request = new RestRequest("Leagues", Method.GET);
                IRestResponse<List<League>> response = _client.Execute<List<League>>(request);

                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }

            });               
        }

        public Task<League> FindLeagueById(Guid id)
        {
            return Task<League>.Factory.StartNew(() =>
            {
                RestRequest request = new RestRequest("Leagues/{guid}", Method.GET);
                request.AddUrlSegment("guid", id.ToString());

                IRestResponse<League> response = _client.Execute<League>(request);
                if (response.IsSuccessful)
                {
                    return response.Data;
                }
                else
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }



        //public Task Insert(League league)
        //{
        //    return Task.Factory.StartNew(()=> {

        //            RestRequest request = new RestRequest("Leagues", Method.POST);
        //request.RequestFormat = DataFormat.Json;
        //            request.AddBody(new { Id = league.Id});

        //            IRestResponse response = _client.Execute(request);
        //        if (!response.IsSuccessful)
        //        {
        //            throw new Exception(response.ErrorMessage);
        //        }
        //    });
        //}

        public Task Update(League league)
        {
            return Task.Factory.StartNew(()=> {
                RestRequest request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(league);
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }
    }
}
