﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.ViewModels;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index(string tournamentId)
        {
            var client = new RestClient($@"https://localhost:44350/api/Tournament/Get/{tournamentId}");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            var tournament = JsonConvert.DeserializeObject<Tournament>(response.Content);



            switch (tournament.TournamentType)
            {
                case TournamentType.League:
                    var leagueViewModel = new LeagueViewModel()
                    {
                        Tournament = tournament
                    };
                    return View("League/Index", leagueViewModel);
                case TournamentType.Cup:
                    var cupViewModel = new CupViewModel()
                    {
                        Tournament = tournament
                    };
                    return View("Cup/Index", cupViewModel);
                default:
                    var tournamentViewModel = new TournamentViewModel()
                    {
                        Tournament = tournament
                    };
                    return View("~/");
            }



        }
    }
}