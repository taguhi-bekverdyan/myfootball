using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Notifications;
using MyFootballAdmin.Main.Views.TournamentEdit;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.Tournaments
{
  public class TournamentsViewModel : BindableBase
  {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly INotificationService _notificationService;

        public TournamentsViewModel(IShellService shellService, IEventAggregator eventAggregator, IRegionManager regionManager, INotificationService notificationService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _notificationService = notificationService;
        }

        private DelegateCommand _addTournamentCommand;

        public DelegateCommand AddTournamentCommand => _addTournamentCommand ?? (_addTournamentCommand = new DelegateCommand(AddTournamentCommandAction));

        public void AddTournamentCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(AddTournamentView).FullName);
        }

        private DelegateCommand _tournamentEditCommand;

        public DelegateCommand TournamentEditCommand => _tournamentEditCommand ?? (_tournamentEditCommand = new DelegateCommand(TournamentEditCommandAction));

        public void TournamentEditCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(TournamentEditView).FullName);
        }

        private DelegateCommand _deleteTournamentCommand;

        public DelegateCommand DeleteTournamentCommand => _deleteTournamentCommand ?? (_deleteTournamentCommand = new DelegateCommand(DeleteTournamentCommandAction));

        public void DeleteTournamentCommandAction()
        {

            _notificationService.ShowNotification(NotificationType.Alert, "Tournament has been deleted.");
        }

    }
}
