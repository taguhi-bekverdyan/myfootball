
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.Helpers;
using Newtonsoft.Json;
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
        private const string Endpoint = "https://localhost:44350/api";
        public string Token { get; set; }
        private readonly RestClient _client;
        public DateTime ExpiresAt { get; set; }
        public MatchService()
        {
            _client = new RestClient(Endpoint);
            Token = AccessToken.Token;
            ExpiresAt = AccessToken.ExpiresAt;
        }
        public async Task Delete(string id)
        {
                RestRequest request = new RestRequest("delete/{id}", Method.DELETE);
                request.AddUrlSegment("id", id.ToString());
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
                {
                    throw new Exception(response.ErrorMessage);
                }
        }

        public async Task<List<Match>> FindAll()
        {
            var request = new RestRequest("match", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Match> matches = JsonConvert.DeserializeObject<List<Match>>(response.Content);
            return matches;
        }

        public async Task<Match> FindMatchById(string id)
        {
            var request = new RestRequest("match/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Match match = JsonConvert.DeserializeObject<Match>(response.Content);
            return match;
        }

        //public async Task<League> GetLeagueByUserId(string accessToken, string id)
        //{
        //    var request = new RestRequest("league/by_user_id/{id}", Method.GET);
        //    request.AddHeader("authorization", $"Bearer {accessToken}");
        //    request.AddUrlSegment("id", id);

        //    IRestResponse response = await _client.ExecuteTaskAsync(request);

        //    League league = JsonConvert.DeserializeObject<League>(response.Content);
        //    return league;

        //}
        public async Task Create(Match match)
        {
            var request = new RestRequest("match/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(match);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Update(Match match)
        {
            var request = new RestRequest("match/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(match);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }


    }
}