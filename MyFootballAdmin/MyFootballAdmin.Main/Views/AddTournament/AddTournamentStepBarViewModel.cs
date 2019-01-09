using MyFootballAdmin.Common.Prism;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddTournamentStepBarViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IShellService _shellService;
        private readonly IRegionManager _regionManager;

        public AddTournamentStepBarViewModel(IShellService shellService, IRegionManager regionManager)
        {
            _shellService = shellService;
            _regionManager = regionManager;
        }

        public IRegionManager RegionManager { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
        }
    }
}
