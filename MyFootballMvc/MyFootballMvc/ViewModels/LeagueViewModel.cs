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
    public string ActiveTab { get; set; }
    public byte[] Logo { get; set; }
    public byte[] Banner { get; set; }

    public LeagueViewModel(string leagueId)
    {
      var client = new RestClient($@"https://localhost:44350/api/Tournament/{leagueId}");
      var request = new RestRequest(Method.GET);
      var response = client.Execute(request);

      Tournament = JsonConvert.DeserializeObject<Tournament>(response.Content);
    }
  }
}
