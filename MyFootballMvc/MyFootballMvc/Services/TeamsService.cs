using System.Collections.Generic;
using System.Threading.Tasks;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;
using Coach = MyFootballMvc.Models.Coach;

namespace MyFootballMvc.Services
{
    public class TeamsService
    {
        private const string Endpoint = "https://localhost:44350/api";
        private readonly RestClient _client;
        private readonly PlayersService _playersService;
        private readonly CoachService _coachesService;
        private readonly StaffService _staffService;

        public TeamsService()
        {
            _client = new RestClient(Endpoint);
            _playersService=new PlayersService();
            _staffService=new StaffService();
            _coachesService = new CoachService();
        }

        public async Task<List<Team>> FindAll(string accessToken)
        {
            var request = new RestRequest("teams", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");

            var response = await _client.ExecuteTaskAsync(request);

            var teams = JsonConvert.DeserializeObject<List<Team>>(response.Content);
            return teams;
        }

        public async Task<Team> FindTeamById(string accessToken, string id)
        {
            var request = new RestRequest("teams/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            var response = await _client.ExecuteTaskAsync(request);

            var team = JsonConvert.DeserializeObject<Team>(response.Content);
            return team;
        }


        public async Task<List<Player>> FindPlayersByTeamId(string accessToken, string id)
        {
            var request = new RestRequest("teams/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            var response = await _client.ExecuteTaskAsync(request);

            var team = JsonConvert.DeserializeObject<Team>(response.Content);
            var players=new List<Player>();
            foreach (var teamPlayer in team.Players)
            {
                players.Add(await _playersService.FindPlayerById(accessToken, teamPlayer.Id));
            }
            return players;
        }

        public async Task<List<Coach>> FindManagersByTeamId(string accessToken, string id)
        {
            var request = new RestRequest("teams/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            var response = await _client.ExecuteTaskAsync(request);

            var team = JsonConvert.DeserializeObject<Team>(response.Content);
            var coaches = new List<Coach>();
            foreach (var teamManager in team.Managers)
            {
                coaches.Add(await _coachesService.FindCoachById(accessToken, teamManager.Id));
            }
            return coaches;
        }

        public async Task<List<Staff>> FindStaffMembersByTeamId(string accessToken, string id)
        {
            var request = new RestRequest("teams/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);
            var response = await _client.ExecuteTaskAsync(request);

            var team = JsonConvert.DeserializeObject<Team>(response.Content);
            var staffMembers = new List<Staff>();
            foreach (var teamStaff in team.StaffMembers)
            {
                staffMembers.Add(await _staffService.FindStaffById(accessToken, teamStaff.Id));
            }
            return staffMembers;
        }



        public async Task<List<Coach>> FindAllManagers()
        {
            var request = new RestRequest("teams/managers", Method.GET);

            var response = await _client.ExecuteTaskAsync(request);

            var managers = JsonConvert.DeserializeObject<List<Coach>>(response.Content);
            return managers;
        }

        public async Task<Team> FindTeamByUserId(string accessToken, string id)
        {
            var request = new RestRequest("teams/by_president_id/{id}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("id", id);

            var response = await _client.ExecuteTaskAsync(request);

            return JsonConvert.DeserializeObject<Team>(response.Content);
        }

        public async Task<Team> FindTeamByName(string accessToken, string name)
        {
            var request = new RestRequest("teams/by_name/{name}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("name", name);

            var response = await _client.ExecuteTaskAsync(request);

            return JsonConvert.DeserializeObject<Team>(response.Content);
        }

        public async Task<Team> FindTeamByShortName(string accessToken, string name)
        {
            var request = new RestRequest("teams/by_short_name/{name}", Method.GET);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddUrlSegment("name", name);

            var response = await _client.ExecuteTaskAsync(request);

            return JsonConvert.DeserializeObject<Team>(response.Content);
        }

        public async Task Insert(string accessToken, Team team)
        {
            var request = new RestRequest("teams/create", Method.POST);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddJsonBody(team);
            var response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Update(string accessToken, Team team)
        {
            var request = new RestRequest("teams/update", Method.PUT);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.AddJsonBody(team);
            var response = await _client.ExecuteTaskAsync(request);
        }

        public async Task Delete(string accessToken, Team team)
        {
            var request = new RestRequest("teams/delete/{id}", Method.DELETE);
            request.AddHeader("authorization", $"Bearer {accessToken}");
            request.RequestFormat = DataFormat.Json;
            var response = await _client.ExecuteTaskAsync(request);
        }
    }
}