using MyFootballMvc.Models;
using MyFootballMvc.Services;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.ViewModels
{
    public class LeagueViewModel : TournamentViewModel
    {
        private LeaguesService _leaguesService { get; }
        public League League { get; set; }

        public LeagueViewModel(string leagueId)
        {
            _leaguesService = new LeaguesService();
            Ctor(leagueId);
        }

        public LeagueViewModel(string leagueId, string token, string id) : base(token, id)
        {
            _leaguesService = new LeaguesService();
            Ctor(leagueId);
        }

        public byte[] Logo { get; set; }
        public byte[] Banner { get; set; }



        private void Ctor(string tournamentId)
        {

            var client = new RestClient($@"https://localhost:44350/api/Tournament/{tournamentId}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            if (response.IsSuccessful)
            {
                Tournament = JsonConvert.DeserializeObject<Tournament>(response.Content);
            }

            League = _leaguesService.FindLeagueByTournamentId(tournamentId).Result;

        }



    }
}