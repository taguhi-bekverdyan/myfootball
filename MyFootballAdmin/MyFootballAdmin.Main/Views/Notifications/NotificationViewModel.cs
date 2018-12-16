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
        }

        private Notification _notification;

        public Notification Notification
        {
            get { return _notification; }
            set { SetProperty(ref _notification, value); }
        }

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

        private void NotificationEventHandler(NotificationEventArgs args)
        {
            this.Notification = args.Notification;
            _eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(NotificationEventHandler);
        }

        public IRegionManager RegionManager { get; set; }
    }
}
