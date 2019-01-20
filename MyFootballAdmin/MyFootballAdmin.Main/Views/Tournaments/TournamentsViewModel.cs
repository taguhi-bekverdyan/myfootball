using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.CupService;
using MyFootballAdmin.Data.Services.LeagueService;
using MyFootballAdmin.Main.Views.AddTournament;
using MyFootballAdmin.Main.Views.Notifications;
using MyFootballAdmin.Main.Views.Pauses;
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
  public class TournamentsViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly IRegionManager _regionManager;
        private readonly INotificationService _notificationService;
        private readonly ILeagueService _leagueService;
        private readonly ICupService _cupService;

        public TournamentsViewModel(IShellService shellService, IEventAggregator eventAggregator, IRegionManager regionManager, INotificationService notificationService, ICupService cupService, ILeagueService leagueService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _notificationService = notificationService;
            _leagueService = leagueService;
            _cupService = cupService;
        }

        private List<League> _leagues;
        public List<League> Leagues
        {
            get { return _leagues; }
            set { SetProperty(ref _leagues, value); }
        }

        private League _selectedLeague;
        public League SelectedLeague
        {
            get { return _selectedLeague; }
            set { SetProperty(ref _selectedLeague, value); }
        }

        private DelegateCommand _addTournamentCommand;

        public DelegateCommand AddTournamentCommand => _addTournamentCommand ?? (_addTournamentCommand = new DelegateCommand(AddTournamentCommandAction));

        public void AddTournamentCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(AddTournamentStepBarView).FullName);
        }

        private DelegateCommand _tournamentEditCommand;

        public DelegateCommand TournamentEditCommand => _tournamentEditCommand ?? (_tournamentEditCommand = new DelegateCommand(TournamentEditCommandAction));

        public void TournamentEditCommandAction()
        {
            //_notificationService.ShowNotification(NotificationType, "");
            NavigationParameters param = new NavigationParameters { { "request", SelectedLeague } };
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(TournamentEditView).FullName,param);
        }

        private DelegateCommand _deleteTournamentCommand;

        public DelegateCommand DeleteTournamentCommand => _deleteTournamentCommand ?? (_deleteTournamentCommand = new DelegateCommand(DeleteTournamentCommandAction));

        public IRegionManager RegionManager { get; set ; }

        public void DeleteTournamentCommandAction()
        {
            _leagueService.Delete(SelectedLeague.Id);
            _notificationService.ShowNotification(NotificationType.Alert, "Tournament has been deleted.");
        }

        public async void FindTournaments()
        {
            List<League> Leagues = new List<League>();
            Leagues = await _leagueService.FindAll();
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            FindTournaments();
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            
        }
    }
}
