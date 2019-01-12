using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddLeagueViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IShellService _shellService;
        private readonly IRegionManager _regionManager;

        public AddLeagueViewModel(IShellService shellService, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationService notificationService)
        {
            _shellService = shellService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
        }

        public IRegionManager RegionManager { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //NavigationParameters param = navigationContext.Parameters["request"];
        }
    }
}
