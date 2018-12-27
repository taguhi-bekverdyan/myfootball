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
      ViewBag.Current = "fixtures";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Fixtures.cshtml", leagueViewModel);
    }
    public IActionResult Results(string tournamentId)
    {
      ViewBag.Current = "results";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Results.cshtml", leagueViewModel);
    }

    public IActionResult Tables(string tournamentId)
    {
      ViewBag.Current = "tables";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Tables.cshtml", leagueViewModel);
    }

    public IActionResult Clubs(string tournamentId)
    {
      ViewBag.Current = "clubs";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Clubs.cshtml", leagueViewModel);
    }

    public IActionResult Players(string tournamentId)
    {
      ViewBag.Current = "players";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Players.cshtml", leagueViewModel);
    }

    public IActionResult Managers(string tournamentId)
    {
      ViewBag.Current = "managers";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Managers.cshtml", leagueViewModel);
    }

    public IActionResult News(string tournamentId)
    {
      ViewBag.Current = "news";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/News.cshtml", leagueViewModel);
    }

    public IActionResult Social(string tournamentId)
    {
      ViewBag.Current = "social";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Social.cshtml", leagueViewModel);
    }

    public IActionResult History(string tournamentId)
    {
      ViewBag.Current = "history";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/History.cshtml", leagueViewModel);
    }

    public IActionResult Referees(string tournamentId)
    {
      ViewBag.Current = "referees";
      var leagueViewModel = new LeagueViewModel(tournamentId);
      return View("~/Views/League/Referees.cshtml", leagueViewModel);
    }
  }
}