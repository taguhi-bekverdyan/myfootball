using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Notifications;
using MyFootballAdmin.Main.Views.Tournaments;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Pauses
{
    public class PausesViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;

        public PausesViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService)
        {
            _shellService = shellService;
            _notificationService = notificationService;
            _eventAggregator = eventAggregator;
        }

        #region Commands
        private DelegateCommand _addPauseCommand;

        public DelegateCommand AddPauseCommand => _addPauseCommand ?? (_addPauseCommand = new DelegateCommand(AddPauseCommandAction));

        public void AddPauseCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _shellService.ShowShell(typeof(AddPauseView).FullName);
        }

        private DelegateCommand _editPauseCommand;

        public DelegateCommand EditPauseCommand => _editPauseCommand ?? (_editPauseCommand = new DelegateCommand(EditPauseCommandAction));

        public void EditPauseCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _shellService.ShowShell(typeof(AddPauseView).FullName);
        }

        private DelegateCommand _deletePauseCommand;

        public DelegateCommand DeletePauseCommand => _deletePauseCommand ?? (_deletePauseCommand = new DelegateCommand(DeletePauseCommandAction));

        public void DeletePauseCommandAction()
        {

            _notificationService.ShowNotification(NotificationType.Alert, "Pause has been deleted.");
        }


        #endregion

        public IRegionManager RegionManager { get; set; }

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

    }
}
