using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Main
{
  public class BesidesToolBarViewModel : BindableBase//, INavigationAware
  {

    private readonly IShellService _shellService;
    private readonly IEventAggregator _eventAggregator;
    private readonly INotificationService _notificationService;
    private readonly IRegionManager _regionManager;

    public BesidesToolBarViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService, IRegionManager regionManager)
    {
      _shellService = shellService;
      _eventAggregator = eventAggregator;
      _notificationService = notificationService;
      _regionManager = regionManager;
    }


    private DelegateCommand _addTournamentCommand;
    public DelegateCommand AddTournamentCommand => _addTournamentCommand ?? (_addTournamentCommand = new DelegateCommand(AddTournamentCommandAction));

    public void AddTournamentCommandAction()
    {
            _notificationService.ShowNotification(NotificationType.Warning, "!");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(AddTournamentView).FullName);
    }

    //public void OnNavigatedTo(NavigationContext navigationContext)
    //{

    //}

    //public bool IsNavigationTarget(NavigationContext navigationContext)
    //{
    //    throw new NotImplementedException();
    //}

    //public void OnNavigatedFrom(NavigationContext navigationContext)
    //{

    //}
  }
}
