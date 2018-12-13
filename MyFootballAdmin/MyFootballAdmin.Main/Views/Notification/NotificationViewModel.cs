using MyFootballAdmin.Common.Prism;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MyFootballAdmin.Main.Views.Main.MainViewModel;

namespace MyFootballAdmin.Main.Views.Notification
{
    public class NotificationViewModel:BindableBase, INavigationAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public NotificationViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        //_eventAggregator.GetEvent<DeviceAddedOrEditedEvent>().Subscribe(DeviceAddedEventHandler, ThreadOption.UIThread);

        private void NotificationEventHandler(NotificationEventArgs args)
        {
            
            _eventAggregator.GetEvent<NotificationEvent>().Unsubscribe(NotificationEventHandler);
        }
    }
}
