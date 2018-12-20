using Microsoft.AspNetCore.Mvc;
using MyFootballMvc.ViewModels;

namespace MyFootballMvc.Controllers
{
    public class ControllerBase : Controller
    {
        public LayoutViewModel LayoutViewModel { get; set; }

        public ControllerBase()
        {
            LayoutViewModel = new LayoutViewModel();
            ViewData["Tournaments"] = LayoutViewModel.Tournaments;
        }
    }



}
