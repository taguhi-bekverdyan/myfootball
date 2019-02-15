using System;
using System.Collections.Generic;
using System.Linq;
using CloudinaryDotNet;
using MyFootballMvc.Interfaces;
using MyFootballMvc.Models;
using MyFootballMvc.Services;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.ViewModels
{
    public class LayoutViewModel : IMenuItem
    {


        public Cloudinary Cloudinary { get; set; }

        public string ActiveMenuItem { get; set; }
        public List<Tournament> Tournaments { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public bool HasTeams { get; set; }
        public bool HasPitches { get; set; }
        public bool IsEditPage { get; set; }
        public string UserImage { get; set; }

        protected readonly UsersService _userSevice;
        protected readonly TeamsService _teamsService;
        protected readonly PitchService _pitchService;

        public LayoutViewModel(string token, string userId) : this()
        {
            _userSevice = new UsersService();
            _teamsService = new TeamsService();
            _pitchService = new PitchService();

            User user = _userSevice.FindUserById(token, userId).Result;
            
            if (user == null)
            {
                UserName = "My profile";
                HasTeams = false;
                HasPitches = false;
                UserImage = null;
            }
            else
            {
                UserName = "Hi " + user.FirstName;
                UserImage = user.Image;
                UserId = user.Id;
            }


            if (_teamsService.FindTeamByUserId(token, userId).Result != null)
            {
                HasTeams = true;
            }
            else
            {
                HasTeams = false;
            }

            if (_pitchService.FindPitchesByUserId(token, userId).Result.Count != 0)
            {
                HasPitches = true;
            }
            else
            {
                HasPitches = false;
            }
        }

        public LayoutViewModel()
        {
            var client = new RestClient("https://localhost:44350/api/Tournament");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Tournaments = JsonConvert.DeserializeObject<List<Tournament>>(response.Content).OrderBy(x => x.Priority).ToList();

            var account = new Account(
                "myfootball-am",
                "146315763856442",
                "39tiuvYatl-1kXLVIMifY1nfSuQ");

            Cloudinary = new Cloudinary(account);
        }
    }
}
