using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class PlayersService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public PlayersService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<List<Player>> FindAll(string accessToken)
        {
            var request = new RestRequest("players", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Player> plyaers = JsonConvert.DeserializeObject<List<Player>>(response.Content);
            return plyaers;
        }

        public async Task<Player> FindUserById(string accessToken, Guid guid)
        {
            var request = new RestRequest("players/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", guid.ToString());
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Player player = JsonConvert.DeserializeObject<Player>(response.Content);
            return player;
        }

    }
}
