using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.Helpers;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.CupService
{
    public class CupService : ICupService
    {
        private const string Endpoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public CupService()
        {
            _client = new RestClient(Endpoint);
            Token = AccessToken.Token;
            ExpiresAt = AccessToken.ExpiresAt;
        }

        public async Task<List<Cup>> FindAll()
        {
            var request = new RestRequest("cup", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Cup> cups = JsonConvert.DeserializeObject<List<Cup>>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return cups;
            }
        }

        public async Task<Cup> FindCupById(string id)
        {
            var request = new RestRequest("cup/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Cup cup = JsonConvert.DeserializeObject<Cup>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return cup;
            }
        }

        public async Task<Cup> FindCupByName(string name)
        {
            var request = new RestRequest("cup/{name}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("name", name);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Cup cup = JsonConvert.DeserializeObject<Cup>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return cup;
            }
        }

        public async Task<Cup> FindCupByStartDate(string startDate)
        {
            var request = new RestRequest("cup/{startDate}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("startDate", startDate);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Cup cup = JsonConvert.DeserializeObject<Cup>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return cup;
            }
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
        public async Task Create(Cup cup)
        {
            var request = new RestRequest("cup/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(cup);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(Cup cup)
        {
            var request = new RestRequest("cup/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(cup);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
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
    }
}
