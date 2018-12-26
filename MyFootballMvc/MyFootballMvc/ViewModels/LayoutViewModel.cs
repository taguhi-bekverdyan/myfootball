﻿using System.Collections.Generic;
using System.Linq;
using MyFootballMvc.Models;
using Newtonsoft.Json;
using RestSharp;

namespace MyFootballMvc.ViewModels
{
    public class LayoutViewModel
    {
        public List<Tournament> Tournaments { get; set; }

        public LayoutViewModel()
        {
            var client = new RestClient("https://localhost:44350/api/Tournament");
            var request = new RestRequest(Method.GET);
            var response = client.Execute(request);

            Tournaments = JsonConvert.DeserializeObject<List<Tournament>>(response.Content).OrderBy(x=>x.Priority).ToList();
        }
    }
}