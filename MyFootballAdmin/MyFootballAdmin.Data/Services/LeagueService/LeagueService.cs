﻿
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.Helpers;
using MyFootballAdmin.Data.Services.LeagueService;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballAdmin.Data.Services.LeagueService
{
    public class LeagueService : ILeagueService
    {
        private const string Endpoint = @"https://localhost:44350/api/";
        private readonly RestClient _client;
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public LeagueService()
        {
            _client = new RestClient(Endpoint);
            Token = AccessToken.Token;
            ExpiresAt = AccessToken.ExpiresAt;
        }

        public async Task<List<League>> FindAll()
        {
            var request = new RestRequest("League", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<League> leagues = JsonConvert.DeserializeObject<List<League>>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return leagues;
            }
        }

        public async Task<League> FindLeagueById(string id)
        {
            var request = new RestRequest("League/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            League league = JsonConvert.DeserializeObject<League>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return league;
            }
        }

        public async Task<League> FindLeagueByName(string name)
        {
            var request = new RestRequest("League/{name}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("name", name);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            League league = JsonConvert.DeserializeObject<League>(response.Content);

            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return league;
            }
        }

        public async Task<League> FindLeagueByStartDate(string startDate)
        {
            var request = new RestRequest("League/{startDate}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("startDate", startDate);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            League league = JsonConvert.DeserializeObject<League>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return league;
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
        public async Task Create(League league)
        {
            var request = new RestRequest("League/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(league);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(League league)
        {
            var request = new RestRequest("League/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(league);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Delete(string id)
        {
            RestRequest request = new RestRequest("delete/League/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }



    }
}
