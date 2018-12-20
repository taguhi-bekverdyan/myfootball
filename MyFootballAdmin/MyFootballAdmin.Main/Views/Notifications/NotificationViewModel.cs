using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Helpers;
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
using static MyFootballAdmin.Main.Views.Main.ToolBarViewModel;

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

        #region Types

        private string _message = "You logged in as Admin.";

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _colour = "Blue";

        public string Colour
        {
            get { return _colour; }
            set { SetProperty(ref _colour, value); }
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
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(NotificationEventHandler);
        }

        #endregion

        private void NotificationEventHandler(NotificationEventArgs args)
        {
            Message = args.Notification.Message;
            Colour = args.Notification.Colour;
        }

        public IRegionManager RegionManager { get; set; }
    }
}
