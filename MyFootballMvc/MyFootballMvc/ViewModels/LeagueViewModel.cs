using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.ViewModels
{
  public class LeagueViewModel : TournamentViewModel
  {
    public byte[] Logo { get; set; }
    public byte[] Banner { get; set; }

    public LeagueViewModel(string leagueId)
    {
        public LeagueViewModel(string leagueId)
        {
            Ctor(leagueId);
        }

        public LeagueViewModel(string leagueId, string token, string id) : base(token,id)
        {
            Ctor(leagueId);
        }

        private void Ctor(string leagueId) {
            var client = new RestClient($@"https://localhost:44350/api/Tournament/{leagueId}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Tournament = JsonConvert.DeserializeObject<Tournament>(response.Content);
        }

    }
  }
}
