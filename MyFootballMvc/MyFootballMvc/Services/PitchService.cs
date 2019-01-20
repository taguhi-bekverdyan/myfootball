using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
  public class PitchService
  {
    private const string Endpoint = "https://localhost:44350/api";
    private readonly RestClient _client;
    public PitchService()
    {
      _client = new RestClient(Endpoint);
    }

    public async Task<List<Pitch>> FindAll(string accessToken)
    {
      var request = new RestRequest("pitch", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      List<Pitch> pitches = JsonConvert.DeserializeObject<List<Pitch>>(response.Content);
      return pitches;
    }

    public async Task<Pitch> FindPitchById(string accessToken, string id)
    {
      var request = new RestRequest("pitch/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);
      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Pitch pitch = JsonConvert.DeserializeObject<Pitch>(response.Content);
      return pitch;
    }

    public async Task<List<Pitch>> FindPitchesByUserId(string accessToken, string id)
    {
      var request = new RestRequest("pitch/by_user_id/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      
      if (response.StatusCode == HttpStatusCode.OK)
      {
        return JsonConvert.DeserializeObject<List<Pitch>>(response.Content);
      }
      return new List<Pitch>();
    }

    public async Task Insert(string accessToken, Pitch pitch)
    {
      var request = new RestRequest("pitch/create", Method.POST);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(pitch);
      IRestResponse response = await _client.ExecuteTaskAsync(request);

    }

    public async Task Update(string accessToken, Pitch pitch)
    {
      var request = new RestRequest("pitch/update", Method.PUT);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddJsonBody(pitch);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }

    public async Task Delete(string accessToken, Pitch pitch)
    {

      var request = new RestRequest("pitch/delete/{id}", Method.DELETE);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.RequestFormat = DataFormat.Json;
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }
  }
}
