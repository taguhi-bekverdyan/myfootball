using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static MyFootballAdmin.Main.Views.Main.MainViewModel;

namespace MyFootballAdmin.Main.Views.Notifications
{
    public class NotificationViewModel:BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public NotificationViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(NotificationEventHandler, ThreadOption.UIThread);
        }

        #region Types

        private Notification _notification = new Notification(NotificationType.Alert, "You logged in as Admin.");

        public Notification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, value); }
        }

        #endregion

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }
 
        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            
        }

        #endregion

        private void NotificationEventHandler(NotificationEventArgs args)
        {
            Notification Notification = new Notification();
            this.Notification = args.Notification;
            _eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(NotificationEventHandler);
        }

        public IRegionManager RegionManager { get; set; }
    }
}
