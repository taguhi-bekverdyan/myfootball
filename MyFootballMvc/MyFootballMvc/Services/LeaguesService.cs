using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Services
{
    public class LeaguesService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;

        public LeaguesService()
        {
            _client = new RestClient(Endpoint);
        }


        public async Task<List<League>> FindAll()
        {
            var request = new RestRequest("League", Method.GET);
            //request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<League> teams = JsonConvert.DeserializeObject<List<League>>(response.Content);
            return teams;
        }


        public async Task Update(string accessToken, League league)
        {
            var request = new RestRequest("League/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddJsonBody(league);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        public async Task<League> FindLeagueByTournamentId(string id)
        {
            var leagues=await FindAll();

            foreach (var league in leagues)
            {
                if (league.Tournament.Id == id)
                    return league;
            }

            return null;
        }
    }
}
