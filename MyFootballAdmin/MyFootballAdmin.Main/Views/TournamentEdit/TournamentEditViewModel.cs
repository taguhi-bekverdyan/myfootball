using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Notifications;
using MyFootballAdmin.Main.Views.Pauses;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.TournamentEdit
{
    public class TournamentEditViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IRegionManager _regionManager;

        public TournamentEditViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService, IRegionManager regionManager)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            _regionManager = regionManager;

        }

        private DelegateCommand _matchesCommand;

        public DelegateCommand MatchesCommand => _matchesCommand ?? (_matchesCommand = new DelegateCommand(MatchesCommandAction));

        public void MatchesCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            //_regionManager.RequestNavigate(RegionNames.PausesAndMatchesRegion, typeof(MatchesView).FullName);
        }

        private DelegateCommand _pausesCommand;

        public DelegateCommand PausesCommand => _pausesCommand ?? (_pausesCommand = new DelegateCommand(PausesCommandAction));

        public void PausesCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _regionManager.RequestNavigate(RegionNames.PausesAndMatchesRegion, typeof(PausesView).FullName);
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
