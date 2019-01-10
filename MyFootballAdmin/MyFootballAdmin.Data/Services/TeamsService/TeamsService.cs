using MyFootballAdmin.Data.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.TeamsService
{
    public class TeamsService: ITeamsService
    {
        private const string EndPoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;

        public TeamsService()
        {
            _client = new RestClient(EndPoint);
        }

        public Task Delete(string id)
        {
            return Task.Factory.StartNew(() => {
                RestRequest request = new RestRequest("Leagues/{id}", Method.DELETE);
                request.AddUrlSegment("id", id.ToString());
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }


        public Task<List<Team>> FindAll()
        {
            return Task<List<Team>>.Factory.StartNew(() => {
                RestRequest request = new RestRequest("Teams", Method.GET);
                IRestResponse<List<Team>> response = _client.Execute<List<Team>>(request);

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

        public Task<Team> FindTeamById(string id)
        {
            return Task<Team>.Factory.StartNew(() =>
            {
                RestRequest request = new RestRequest("Teams/{id}", Method.GET);
                request.AddUrlSegment("id", id);

                IRestResponse<Team> response = _client.Execute<Team>(request);
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



        public Task Create(Team team)
        {
            return Task.Factory.StartNew(() =>
            {

                RestRequest request = new RestRequest(Method.POST);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(new { team });

                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }

        public Task Update(Team team)
        {
            return Task.Factory.StartNew(() => {
                RestRequest request = new RestRequest(Method.PUT);
                request.RequestFormat = DataFormat.Json;
                request.AddBody(team);
                IRestResponse response = _client.Execute(request);
                if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
            });
        }
    }
}
