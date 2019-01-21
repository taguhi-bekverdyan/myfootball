using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyFootballMvc.Services
{
    public class StaffService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        public StaffService()
        {
            _client = new RestClient(Endpoint);
        }

        public async Task<List<Staff>> FindAll(string accessToken)
        {
            var request = new RestRequest("staff", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Staff> staff = JsonConvert.DeserializeObject<List<Staff>>(response.Content);
            return staff;
        }

        public async Task<Staff> FindStaffById(string accessToken, string id)
        {
            var request = new RestRequest("staff/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Staff staff = JsonConvert.DeserializeObject<Staff>(response.Content);
            return staff;
        }

        public async Task<List<Staff>> FindFreeStafs(string accessToken)
        {
            var request = new RestRequest("staff/free_staffs", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            IRestResponse response = await _client.ExecuteTaskAsync(request);

            List<Staff> staffs = JsonConvert.DeserializeObject<List<Staff>>(response.Content);
            return staffs;
        }

        public async Task<Staff> GetStaffByUserId(string accessToken, string id)
        {
            var request = new RestRequest("staff/by_user_id/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            IRestResponse response = await _client.ExecuteTaskAsync(request);

            Staff staff = JsonConvert.DeserializeObject<Staff>(response.Content);
            return staff;

        }
        public async Task Insert(string accessToken, Staff staff)
        {
            var request = new RestRequest("staff/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(staff);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Update(string accessToken, Staff staff)
        {
            var request = new RestRequest("staff/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            request.AddBody(staff);
            IRestResponse response = await _client.ExecuteTaskAsync(request);
        }
    }
}
