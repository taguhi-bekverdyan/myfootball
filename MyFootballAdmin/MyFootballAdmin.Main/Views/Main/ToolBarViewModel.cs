using System;
using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Tournaments;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using MyFootballAdmin.Main.Views.Error;
using MyFootballAdmin.Main.Views.Pauses;

namespace MyFootballAdmin.Main.Views.Main
{
    public class ToolBarViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;

        public ToolBarViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
  
        }

        public IRegionManager RegionManager { get; set; }

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        #region Commands

        private DelegateCommand _mainViewCommand;

        public DelegateCommand MainViewCommand => _mainViewCommand ?? (_mainViewCommand = new DelegateCommand(MainViewCommandAction));

        public void MainViewCommandAction()
        {
            //RegionManager.RequestNavigate(RegionNames.MainRegion, nameof(MainView));
        }

        private DelegateCommand _tournamentsCommand;

        public DelegateCommand TournamentsCommand => _tournamentsCommand ?? (_tournamentsCommand = new DelegateCommand(TournamentsCommandAction));

        public void TournamentsCommandAction()
        {
            RegionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, nameof(TournamentsView));
        }

        #endregion


    }
}
