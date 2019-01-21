﻿using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.Helpers;
using Newtonsoft.Json;
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
        private const string Endpoint = @"https://localhost:44350/api/";
        public string Token { get; set; }
        private readonly RestClient _client;
        public DateTime ExpiresAt { get; set; }
        public TeamsService()
        {
            _client = new RestClient(Endpoint);
            Token = AccessToken.Token;
            ExpiresAt = AccessToken.ExpiresAt;
        }

        public async Task Delete(string id)
        {
            RestRequest request = new RestRequest("delete/Team/{id}", Method.DELETE);
            request.AddUrlSegment("id", id.ToString());
            IRestResponse response =await  _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task<List<Team>> FindAll()
        {
            var request = new RestRequest("Team", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Team> teams = JsonConvert.DeserializeObject<List<Team>>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return teams;
            }
        }

        public async Task<Team> FindTeamById(string id)
        {
            var request = new RestRequest("Team/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Team team = JsonConvert.DeserializeObject<Team>(response.Content);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
            else
            {
                return team;
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
        public async Task Create(Team team)
        {
            var request = new RestRequest("Team/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(team);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }

        public async Task Update(Team team)
        {
            var request = new RestRequest("Team/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {Token}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(team);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }
        }


    }
}