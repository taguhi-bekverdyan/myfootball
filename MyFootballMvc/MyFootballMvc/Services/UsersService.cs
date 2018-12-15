using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class UsersService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public UsersService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<List<User>> FindAll(string accessToken)
        {            
            var request = new RestRequest("users",Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<User> users = JsonConvert.DeserializeObject<List<User>>(response.Content);
            return users;
        }

        public async Task<User> FindUserById(string accessToken,string id)
        {
            var request = new RestRequest("users/{id}",Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id",id);
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            User user = JsonConvert.DeserializeObject<User>(response.Content);
            return user;
        }

        public async Task Insert(string accessToken,User user)
        {
            var request = new RestRequest("users", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
            
        }

        public async Task Update(string accessToken, User user)
        {
            var request = new RestRequest("users", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(user);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        

    }
}
