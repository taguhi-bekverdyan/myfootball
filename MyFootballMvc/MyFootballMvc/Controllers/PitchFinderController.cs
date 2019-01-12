using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
  public class PitchFinderController : Controller
  {
    public IActionResult Index()
    {
      return View(new PitchFinderViewModel { ActiveItem = "pitchfinder" });
    }
  }
}