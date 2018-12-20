using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class TeamsService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public TeamsService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<List<Team>> FindAll(string accessToken)
        {
            var request = new RestRequest("teams", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(response.Content);
            return teams;
        }

        public async Task<Team> FindUserById(string accessToken, Guid guid)
        {
            var request = new RestRequest("teams/{guid}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("guid", guid.ToString());
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Team team = JsonConvert.DeserializeObject<Team>(response.Content);
            return team;
        }
    }
}
