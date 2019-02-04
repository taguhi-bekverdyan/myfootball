using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.Helpers;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballAdmin.Data.Services.TournamentService
{
    public class TournamentService : ITournamentService
    {
        private const string Endpoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public TournamentService()
        {
            _client = new RestClient(Endpoint);
            Token = AccessToken.Token;
            ExpiresAt = AccessToken.ExpiresAt;
        }
        public async Task Create(Tournament tournament)
        {
            var request = new RestRequest("Tournament/Create", Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(tournament);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(Tournament tournament)
        {
            var request = new RestRequest("Tournament/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(tournament);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public Task Delete(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Tournament>> FindAll()
        {
            var request = new RestRequest("Tournament", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Tournament> leagues = JsonConvert.DeserializeObject<List<Tournament>>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return leagues;
            }
        }

        public Task<Tournament> FindTournamentById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Tournament> FindTournamentByName(string name)
        {
            var request = new RestRequest("Tournament/Get/{name}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("name", name);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            var league = JsonConvert.DeserializeObject<Tournament>(response.Content);

            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return league;
            }
        }

        public Task<Tournament> FindTournamentByStartDate(string startDate)
        {
            throw new NotImplementedException();
        }
    }
}
