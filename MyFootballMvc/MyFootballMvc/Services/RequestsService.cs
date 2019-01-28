using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class RequestsService
    {

        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public RequestsService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<List<Request>> FindAll(string accessToken)
        {
            var request = new RestRequest("requests", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Request> requests = JsonConvert.DeserializeObject<List<Request>>(response.Content);
            return requests;
        }

        public async Task<Request> FindRequestById(string accessToken, string id)
        {
            var request = new RestRequest("requests/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Request req = JsonConvert.DeserializeObject<Request>(response.Content);
            return req;

        }

        public async Task<List<Request>> FindRequestsByTeamId(string accessToken, string id)
        {
            var request = new RestRequest("requests/by_team_id/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Request> requests = JsonConvert.DeserializeObject<List<Request>>(response.Content);
            return requests;
        }

        public async Task<List<Request>> GetRequestsByUserId(string accessToken, string id)
        {
            var request = new RestRequest("requests/by_user_id/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Request> req = JsonConvert.DeserializeObject<List<Request>>(response.Content);
            return req;
        }

        public async Task Insert(string accessToken, Request req)
        {
            var request = new RestRequest("requests/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(req);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Update(string accessToken, Request req)
        {
            var request = new RestRequest("requests/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(req);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }



    }
}
