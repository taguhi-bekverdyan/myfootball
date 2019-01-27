using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
  public class RefereeService
  {
    private const string Endpoint = "https://localhost:44350/api";
    private readonly RestClient _client;
    public RefereeService()
    {
      _client = new RestClient(Endpoint);
    }

    public async Task<List<Referee>> FindAll(string accessToken)
    {
      var request = new RestRequest("referee", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      List<Referee> referee = JsonConvert.DeserializeObject<List<Referee>>(response.Content);
      return referee;
    }

    public async Task<Referee> FindRefereeById(string accessToken, string id)
    {
      var request = new RestRequest("referee/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Referee referee = JsonConvert.DeserializeObject<Referee>(response.Content);
      return referee;
    }

    public async Task<Referee> GetRefereeByUserId(string accessToken, string id)
    {
      var request = new RestRequest("referee/by_user_id/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Referee referee = JsonConvert.DeserializeObject<Referee>(response.Content);
      return referee;

    }
    public async Task Insert(string accessToken, Referee referee)
    {
      var request = new RestRequest("referee/create", Method.POST);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(referee);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }

    public async Task Update(string accessToken, Referee referee)
    {
      var request = new RestRequest("referee/update", Method.PUT);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(referee);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }
  }
}
