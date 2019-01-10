
using MyFootballAdmin.Data.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Match = MyFootballAdmin.Data.Models.Match;

namespace MyFootballAdmin.Data.Services.MatchService
{
    public class MatchService: IMatchService
    {
        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public MatchService()
        {
            _client = new RestClient(EndPoint);
        }

        public Task Delete(string id)
        {
            return Task.Factory.StartNew(() => {
                RestRequest request = new RestRequest("Delete/{id}", Method.DELETE);
                request.AddUrlSegment("id", id.ToString());
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }


        public Task<List<Match>> FindAll()
        {
            return Task<List<Match>>.Factory.StartNew(() => {
                RestRequest request = new RestRequest("Matches", Method.GET);
                IRestResponse<List<Match>> response = _client.Execute<List<Match>>(request);

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

        public Task<Match> FindMatchById(string id)
        {
            return Task<Match>.Factory.StartNew(() =>
            {
                RestRequest request = new RestRequest("Matches/{id}", Method.GET);
                request.AddUrlSegment("guid", id.ToString());

                IRestResponse<Match> response = _client.Execute<Match>(request);
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



        public Task Create(Match match)
        {
            return Task.Factory.StartNew(() =>
            {

                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { match });
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }

        public Task Update(Match match)
        {
            return Task.Factory.StartNew(() => {
                RestRequest request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(match);
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }

    }
}
