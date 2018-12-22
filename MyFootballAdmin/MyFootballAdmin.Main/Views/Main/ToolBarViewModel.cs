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
using Microsoft.Practices.ServiceLocation;

namespace MyFootballAdmin.Main.Views.Main
{
  public class ToolBarViewModel : BindableBase, INavigationAware
  {
    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private readonly INotificationService _notificationService;
    private readonly IRegionManager _regionManager;

    public ToolBarViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService, IRegionManager regionManager)
    {
      _shellService = shellService;
      _eventAggregator = eventAggregator;
      _notificationService = notificationService;
      _regionManager = regionManager;
    }



    #region Navigation

    public void OnNavigatedTo(NavigationContext navigationContext)
    {

    }

    public bool IsNavigationTarget(NavigationContext navigationContext)
    {
      return true;
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
            _notificationService.ShowNotification(NotificationType.Info,"Main View");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(BesidesToolBarView).FullName);
    }

    private DelegateCommand _tournamentsCommand;

    public DelegateCommand TournamentsCommand => _tournamentsCommand ?? (_tournamentsCommand = new DelegateCommand(TournamentsCommandAction));

    public void TournamentsCommandAction()
    {
            //_notificationService.ShowNotification(NotificationType, "");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(TournamentsView).FullName);
    }

    #endregion


  }
}
