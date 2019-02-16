using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class CoachService
    {
        public TeamsService _teamsService;
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public CoachService()
        {
            _client = new RestClient(Endpoint);
            _teamsService = new TeamsService();
        }

        public async Task<List<Coach>> FindAll(string accessToken)
        {
            var request = new RestRequest("coach", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Coach> coaches = JsonConvert.DeserializeObject<List<Coach>>(response.Content);
            return coaches;
        }

       

            public async Task<Coach> FindCoachById(string accessToken,string id)
        {
            var request = new RestRequest("coach/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id",id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Coach coach = JsonConvert.DeserializeObject<Coach>(response.Content);
            return coach;
        }

        public async Task<List<Coach>> FindFreeCoaches(string accessToken,string id)
        {
            var request = new RestRequest("coach/free_coaches/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Coach> coaches = JsonConvert.DeserializeObject<List<Coach>>(response.Content);
            return coaches;
        }

        public async Task<Coach> GetCoachByUserId(string accessToken, string id)
        {
            var request = new RestRequest("coach/by_user_id/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Coach coach = JsonConvert.DeserializeObject<Coach>(response.Content);
            return coach;

        }

        public async Task Insert(string accessToken, Coach coach)
        {
            var request = new RestRequest("coach/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddJsonBody(coach);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Update(string accessToken, Coach coach)
        {
            var request = new RestRequest("coach/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddJsonBody(coach);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }


    }
}
