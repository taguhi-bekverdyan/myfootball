using System;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.Models;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class TournamentController : Controller
    {
        public IActionResult Index(string tournamentId)
        {
            return View(new TournamentViewModel());
        }
    }
}