using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class MatchService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public MatchService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<Match> FindMatchById(string accessToken, string id, string leagueId)
        {
            var request = new RestRequest("match/{leagueId}/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("leagueId", leagueId);
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Match match = JsonConvert.DeserializeObject<Match>(response.Content);
            return match;
        }
    }
}
