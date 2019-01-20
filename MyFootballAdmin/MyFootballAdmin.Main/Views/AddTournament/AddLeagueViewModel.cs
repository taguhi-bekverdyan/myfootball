using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Data.Models;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddLeagueViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IShellService _shellService;
        private readonly IRegionManager _regionManager;

        public AddLeagueViewModel(IShellService shellService, IRegionManager regionManager, IEventAggregator eventAggregator, INotificationService notificationService)
        {
            _shellService = shellService;
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            LeagueToGenerate = new League();
            LeagueToGenerate.StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            LeagueToGenerate.EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);
            SelectedPause = new Pause();
            Pauses = new ObservableCollection<Pause>();
            DayOfWeekChekeds = new ObservableCollection<DayOfWeekCheked>();
            DayOfWeekCheked d;
            for (int i = 0; i <= 6; ++i)
            {
                d = new DayOfWeekCheked();
                d.DayOfWeek = (DayOfWeek)i;
                d.IsCheked = false;
                DayOfWeekChekeds.Add(d);
            }
        }

        #region Types
        private League _leagueToGenerate;

        public League LeagueToGenerate
        {
            get { return _leagueToGenerate; }
            set { SetProperty(ref _leagueToGenerate, value); }
        }
        private DateTime _startPause;
        public DateTime StartPause
        {
            get { return _startPause; }
            set { SetProperty(ref _startPause, value); }
        }

        private DateTime _endPause;
        public DateTime EndPause
        {
            get { return _endPause; }
            set { SetProperty(ref _endPause, value); }
        }
        private Pause _selectedPause;
        public Pause SelectedPause
        {
            get { return _selectedPause; }
            set { SetProperty(ref _selectedPause, value); }
        }
        private ObservableCollection<Pause> _pauses;
        public ObservableCollection<Pause> Pauses
        {
            get { return _pauses; }
            set { SetProperty(ref _pauses, value); }
        }
        private ObservableCollection<DayOfWeekCheked> _dayOfWeekChekeds;
        public ObservableCollection<DayOfWeekCheked> DayOfWeekChekeds
        {
            get { return _dayOfWeekChekeds; }
            set { SetProperty(ref _dayOfWeekChekeds, value); }
        }
        #endregion

        #region Commands
        private DelegateCommand _nextCommand;
        private DelegateCommand<SelectedDatesCollection> _selectionCommand;
        private DelegateCommand<Pause> _editCommand;
        private DelegateCommand _addCommand;
        private DelegateCommand _deleteCommand;
        private DelegateCommand _checkCommand;
        public DelegateCommand DeleteCommand => _deleteCommand ?? (_deleteCommand = new DelegateCommand(DeleteCommandAction));
        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandAction));
        public DelegateCommand CheckCommand => _checkCommand ?? (_checkCommand = new DelegateCommand(CheckCommandAction));
        public DelegateCommand<SelectedDatesCollection> SelectionCommand => _selectionCommand ?? (_selectionCommand = new DelegateCommand<SelectedDatesCollection>(SelectionCommandAction));
        public DelegateCommand<Pause> EditCommand => _editCommand ?? (_editCommand = new DelegateCommand<Pause>(EditCommandAction));

        public DelegateCommand NextCommand => _nextCommand ?? (_nextCommand = new DelegateCommand(NextCommandAction));

        public void CheckCommandAction()
        {
            MessageBox.Show("OK");
        }

        public void DeleteCommandAction()
        {
            Pauses.Remove(SelectedPause);
        }

        public void AddCommandAction()
        {
            Pause p = new Pause();
            p.PauseStart = StartPause;
            p.PauseEnd = EndPause;
            Pauses.Add(p);
        }

        public void NextCommandAction()
        {
            foreach (var p in DayOfWeekChekeds)
            {
                if (p.IsCheked)
                {
                    LeagueToGenerate.MatchDays.Add(p.DayOfWeek);
                }
            }

            LeagueToGenerate.Pauses = Pauses.ToList<Pause>();
            NavigationParameters param;
            param = new NavigationParameters { { "request", LeagueToGenerate } };
            _regionManager.RequestNavigate(RegionNames.AddTournamentRegion, typeof(GenerateLeagueView).FullName, param);
        }

        public void SelectionCommandAction(SelectedDatesCollection d)
        {
            StartPause = d[0];
            EndPause = d[d.Count - 1];
        }

        public void EditCommandAction(Pause selectedPause)
        {
            Pauses.Remove(selectedPause);
            StartPause = selectedPause.PauseStart;
            EndPause = selectedPause.PauseEnd;
        }
        #endregion

        public IRegionManager RegionManager { get; set; }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            var param = navigationContext.Parameters["request"];
            LeagueToGenerate.Tournament = (Tournament)param;
        }
    }
}