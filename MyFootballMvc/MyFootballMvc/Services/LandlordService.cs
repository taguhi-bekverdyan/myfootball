using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
  public class LandlordService
  {
    private const string Endpoint = "https://localhost:44350/api";
    private readonly RestClient _client;
    public LandlordService()
    {
      _client = new RestClient(Endpoint);
    }

    public async Task<List<Landlord>> FindAll(string accessToken)
    {
      var request = new RestRequest("landlord", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      List<Landlord> landlord = JsonConvert.DeserializeObject<List<Landlord>>(response.Content);
      return landlord;
    }

    public async Task<Landlord> FindLandlordById(string accessToken, string id)
    {
      var request = new RestRequest("landlord/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Landlord landlord = JsonConvert.DeserializeObject<Landlord>(response.Content);
      return landlord;
    }

    public async Task<Landlord> GetLandlordByUserId(string accessToken, string id)
    {
      var request = new RestRequest("landlord/by_user_id/{id}", Method.GET);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.AddUrlSegment("id", id);

      IRestResponse response = await _client.ExecuteTaskAsync(request);

      Landlord landlord = JsonConvert.DeserializeObject<Landlord>(response.Content);
      return landlord;

    }
    public async Task Insert(string accessToken, Landlord landlord)
    {
      var request = new RestRequest("landlord/create", Method.POST);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.RequestFormat = DataFormat.Json;
      request.AddBody(landlord);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }

    public async Task Update(string accessToken, Landlord landlord)
    {
      var request = new RestRequest("landlord/update", Method.PUT);
      request.AddHeader("authorization", $"Bearer {accessToken}");
      request.RequestFormat = DataFormat.Json;
      request.AddBody(landlord);
      IRestResponse response = await _client.ExecuteTaskAsync(request);
    }
  }
}
