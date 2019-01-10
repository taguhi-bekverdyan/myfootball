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
      leagueViewModel.ActiveTab = "fixtures";
      return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
    }
    public IActionResult Results(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "results";
      return View("~/Views/League/Results.cshtml", leagueViewModel);
    }

    public IActionResult Tables(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "tables";
      return View("~/Views/League/Tables.cshtml", leagueViewModel);
    }

    public IActionResult Clubs(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "clubs";
      return View("~/Views/League/Clubs.cshtml", leagueViewModel);
    }

    public IActionResult Players(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "players";
      return View("~/Views/League/Players.cshtml", leagueViewModel);
    }

    public IActionResult Managers(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "managers";
      return View("~/Views/League/Managers.cshtml", leagueViewModel);
    }

    public IActionResult News(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "news";
      return View("~/Views/League/News.cshtml", leagueViewModel);
    }

    public IActionResult Social(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "social";
      return View("~/Views/League/Social.cshtml", leagueViewModel);
    }

    public IActionResult History(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "history";
      return View("~/Views/League/History.cshtml", leagueViewModel);
    }

    public IActionResult Referees(string tournamentId)
    {
      var leagueViewModel = new LeagueViewModel(tournamentId);
      leagueViewModel.ActiveTab = "referees";
      return View("~/Views/League/Referees.cshtml", leagueViewModel);
    }
  }
}