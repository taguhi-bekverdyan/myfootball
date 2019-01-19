using MyFootballMvc.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class EmailService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public EmailService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task Insert(string accessToken, Email email)
        {
            var request = new RestRequest("Email/SendEmail", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(email);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }
    }
}
