using System;
using System.Configuration;
using System.Text;
using Auth0.OidcClient;
using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Notification;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;

namespace MyFootballAdmin.Main.Views.Main
{
    public class MainViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public MainViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
        }

        private Notification.Notification _notification;

        public Notification.Notification notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, value); }
        }

        public IRegionManager RegionManager { get; set; }

        #region Navigations

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

        private DelegateCommand _addTournamentCommand;

        public DelegateCommand AddTournamentCommand => _addTournamentCommand ?? (_addTournamentCommand = new DelegateCommand(AddTournamentCommandAction));

        public void AddTournamentCommandAction()
        {
            RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(AddTournamentView));
            Notification.Notification notification = new Notification.Notification(NotificationType.Info,"Teams count must be over 4.");
            _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs { Notification = notification });
        }

        #endregion

        #region Event

        public class NotificationEvent : PubSubEvent<NotificationEventArgs> { }

        public class NotificationEventArgs
        {
            public Notification.Notification Notification { get; set; }
        }

        #endregion

    }
}
