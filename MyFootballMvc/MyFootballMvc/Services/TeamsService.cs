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

    public async Task<Team> FindTeamById(string accessToken, string id)
    {
      var request = new RestRequest("teams/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);
      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Team team = JsonConvert.DeserializeObject<Team>(response.Content);
      return team;
    }

    public async Task<Team> FindTeamByUserId(string accessToken, string id)
    {
      var request = new RestRequest("teams/by_president_id/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      return JsonConvert.DeserializeObject<Team>(response.Content);
    }

    public async Task<Team> FindTeamByName(string accessToken, string name)
    {
      var request = new RestRequest("teams/by_name/{name}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("name", name);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      return JsonConvert.DeserializeObject<Team>(response.Content);
    }

    public async Task Insert(string accessToken, Team team)
    {
      var request = new RestRequest("teams/create", Method.POST);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(team);
      IRestResponse response = await _client.ExecuteTaskAsync(request);

    }

    public async Task Update(string accessToken, Team team)
    {
      var request = new RestRequest("teams/update", Method.PUT);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(team);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }

    public async Task Delete(string accessToken, Team team)
    {

      var request = new RestRequest("teams/delete/{id}", Method.DELETE);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.RequestFormat = DataFormat.Json;
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }

  }
}
