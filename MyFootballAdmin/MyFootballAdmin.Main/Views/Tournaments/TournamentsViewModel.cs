using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Data.Services.CupService;
using MyFootballAdmin.Data.Services.LeagueService;
using MyFootballAdmin.Data.Services.TournamentService;
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
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
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
        private readonly ITournamentService _tournamentService;

        public TournamentsViewModel(IShellService shellService, 
                                    IEventAggregator eventAggregator, 
                                    IRegionManager regionManager, 
                                    INotificationService notificationService, 
                                    ICupService cupService, 
                                    ILeagueService leagueService,
                                    ITournamentService tournamentService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _regionManager = regionManager;
            _notificationService = notificationService;
            _leagueService = leagueService;
            _cupService = cupService;
            _tournamentService = tournamentService;
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

        public void DeleteTournamentCommandAction()
        {
            _leagueService.Delete(SelectedLeague.Id);
            _notificationService.ShowNotification(NotificationType.Alert, "Tournament has been deleted.");
        }

        private DelegateCommand<object> _generateCommand;

        public DelegateCommand<object> GenerateCommand => _generateCommand ?? (_generateCommand = new DelegateCommand<object>(GenerateCommandAction));

        public async void GenerateCommandAction(object league)
        {
            //List<Team> Clubs = new List<Team>();
            //Team club = new Team();
            //club.Name = "LIV";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "MCI";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "TOT";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "CHE";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "ARS";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "MUN";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "EVE";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "BOU";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "LEI";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "WOL";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "WHU";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "WAT";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "BHA";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "CAR";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "NEW";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "CRY";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "BUR";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "HUD";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "SOU";
            //Clubs.Add(club);
            //club = new Team();
            //club.Name = "FUL";
            //Clubs.Add(club);
            //LeagueToGenerate.Teams = Clubs;
            //LeagueToGenerate.Generate();

            if (league is League leagueToGenerate && leagueToGenerate.Teams?.Any() == true)
            {
                //LeagueToGenerate.Generate();
                await _tournamentService.Update(leagueToGenerate.Tournament);
                leagueToGenerate.Tournament = await _tournamentService.FindTournamentByName(leagueToGenerate.Tournament.Name);
                await _leagueService.Update(leagueToGenerate);
            }
        }

        public IRegionManager RegionManager { get; set ; }

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

        public async void FindTournaments()
        {
            Leagues = await _leagueService.FindAll();
        }
    }
}
