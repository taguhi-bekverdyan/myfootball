using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
  public class LeagueController : Controller
  {
    public IActionResult Index(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Index.cshtml", leagueViewModel);
    }

    public IActionResult Fixtures(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "fixtures";
      return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
    }

    public IActionResult Results(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "results";
      return View("~/Views/League/Results.cshtml", leagueViewModel);
    }

    public IActionResult Tables(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "tables";
      return View("~/Views/League/Tables.cshtml", leagueViewModel);
    }

    public IActionResult Clubs(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "clubs";
      return View("~/Views/League/Clubs.cshtml", leagueViewModel);
    }

    public IActionResult Players(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "players";
      return View("~/Views/League/Players.cshtml", leagueViewModel);
    }

    public IActionResult Managers(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "managers";
      return View("~/Views/League/Managers.cshtml", leagueViewModel);
    }

    public IActionResult News(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "news";
      return View("~/Views/League/News.cshtml", leagueViewModel);
    }

    public IActionResult Social(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "social";
      return View("~/Views/League/Social.cshtml", leagueViewModel);
    }

    public IActionResult History(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "history";
      return View("~/Views/League/History.cshtml", leagueViewModel);
    }

    public IActionResult Referees(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveItem = "referees";
      return View("~/Views/League/Referees.cshtml", leagueViewModel);
    }
  }
}