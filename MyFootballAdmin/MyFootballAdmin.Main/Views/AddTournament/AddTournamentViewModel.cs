using Microsoft.Win32;
using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Main;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;
using MyFootballAdmin.Data.Models;
using System.Collections.ObjectModel;
using MyFootballAdmin.Data.Services.TournamentService;
using System.Runtime.Serialization.Json;
using MyFootballAdmin.Data.Services.LeagueService;
using System.ComponentModel;
using System.Windows.Data;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddTournamentViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IRegionManager _regionManager;
        private readonly ITournamentService _tournamentService;
        private readonly ILeagueService _leagueService;

        private ObservableCollection<PauseUIModel> Pauses;

        public AddTournamentViewModel(IShellService shellService, 
                                      IEventAggregator eventAggregator, 
                                      INotificationService notificationService, 
                                      IRegionManager regionManager,
                                      ITournamentService tournamentService,
                                      ILeagueService leagueService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            _regionManager = regionManager;
            _tournamentService = tournamentService;
            _leagueService = leagueService;

            Initialize();
        }

        private void Initialize()
        {
            DaysOfWeek = new ObservableCollection<DayOfWeekCheked>();

            for (int dayNumber = 0; dayNumber < 7; dayNumber++)
            {
                DaysOfWeek.Add(new DayOfWeekCheked() { DayOfWeek = (DayOfWeek)dayNumber });
            }

            Pauses = new ObservableCollection<PauseUIModel>();

            PauseCollectionView = CollectionViewSource.GetDefaultView(Pauses);
            PauseCollectionView.SortDescriptions.Add(new SortDescription("PauseStart", ListSortDirection.Ascending));

            Rules = new ObservableCollection<Rule>();
        }

        private void RefreshPauses(object sender, EventArgs e)
        {
            PauseCollectionView.Refresh();
        }

        #region Types

        private List<Tournament> _tournaments;

        public List<Tournament> Tournaments
        {
            get { return _tournaments; }
            set { SetProperty(ref _tournaments, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }     

        private int _priority;

        public int Priority
        {
            get { return _priority; }
            set { SetProperty(ref _priority, value); }
        }

        //private string _imagePath;

        //public string ImagePath
        //{
        //    get { return _imagePath; }
        //    set { SetProperty(ref _imagePath, value); }
        //}

        private TournamentType _tournamentType;

        public TournamentType TournamentType
        {
            get { return _tournamentType; }
            set { SetProperty(ref _tournamentType, value); }
        }

        private DateTime startDate = DateTime.Now;

        public DateTime StartDate
        {
            get { return startDate; }
            set { SetProperty(ref startDate, value); }
        }

        private DateTime endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return endDate; }
            set { SetProperty(ref endDate, value); }
        }

        public ObservableCollection<DayOfWeekCheked> DaysOfWeek { get; set; }

        public ICollectionView PauseCollectionView { get; set; }

        public ObservableCollection<Rule> Rules { get; set; }

        private int oneHalfTime;

        public int OneHalfTime
        {
            get { return oneHalfTime; }
            set { SetProperty(ref oneHalfTime, value); }
        }

        private int breakTime;

        public int BreakTime
        {
            get { return breakTime; }
            set { SetProperty(ref breakTime, value); }
        }

        private int playersCount;

        public int PlayersCount
        {
            get { return playersCount; }
            set { SetProperty(ref playersCount, value); }
        }

        private int yelloCardsToDisqualification;

        public int YelloCardsToDisqualification
        {
            get { return yelloCardsToDisqualification; }
            set { SetProperty(ref yelloCardsToDisqualification, value); }
        }

        private int countOfMatches;

        public int CountOfMatches
        {
            get { return countOfMatches; }
            set { SetProperty(ref countOfMatches, value); }
        }

        #endregion


        public IRegionManager RegionManager { get; set; }

        #region Commands

        private DelegateCommand<string> _selectTypeCommand;

        public DelegateCommand<string> SelectTypeCommand => _selectTypeCommand ?? (_selectTypeCommand = new DelegateCommand<string>(SelectTypeCommandAction));

        private void SelectTypeCommandAction(string type)
        {
            Enum.TryParse(type, out TournamentType tournamentType);
            TournamentType = tournamentType;
        }

        private DelegateCommand addPauseCommand;

        public DelegateCommand AddPauseCommand => addPauseCommand ?? (addPauseCommand = new DelegateCommand(AddPauseCommandAction));

        private void AddPauseCommandAction()
        {
            var newPause = new PauseUIModel();
            newPause.Updated += RefreshPauses;
            Pauses.Add(newPause);
        }

        private DelegateCommand<object> deletePauseCommand;

        public DelegateCommand<object> DeletePauseCommand => deletePauseCommand ?? (deletePauseCommand = new DelegateCommand<object>(DeletePauseCommandAction));

        private void DeletePauseCommandAction(object pause)
        {
            Pauses.Remove(pause as PauseUIModel);
        }

        private DelegateCommand cancelCommand;

        public DelegateCommand CancelCommand => cancelCommand ?? (cancelCommand = new DelegateCommand(CancelCommandAction));

        private void CancelCommandAction()
        {
            _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(BesidesToolBarView).FullName);
        }

        private DelegateCommand saveCommand;

        public DelegateCommand SaveCommand => saveCommand ?? (saveCommand = new DelegateCommand(SaveCommandAction));

        private async void SaveCommandAction()
        {
            Tournament Tournament = new Tournament();
            Tournament.Name = Name;
            Tournament.Priority = Priority;
            Tournament.TournamentType = TournamentType;
            //todo: add logo uploading

            var existingTournaments = await _tournamentService.FindAll();

            if(existingTournaments.Any(t => t.Name == Tournament.Name))
            {
                //todo show validation or message box
                return;
            }

            if (TournamentType == TournamentType.League)
            {
                var League = new League();

                await _tournamentService.Create(Tournament);
                League.Tournament = await _tournamentService.FindTournamentByName(Tournament.Name);

                League.StartDate = StartDate;
                League.EndDate = EndDate;
                League.CountOfMatches = CountOfMatches;
                League.PlayersCount = PlayersCount;
                League.OneHalfTime = OneHalfTime;
                League.BreakTime = BreakTime;
                League.YelloCardsToDisqualification = YelloCardsToDisqualification;

                League.MatchDays = DaysOfWeek.Where(d => d.IsCheked).Select(d => d.DayOfWeek).ToList();
                League.Pauses = Pauses.Select(p => new Pause() { PauseStart = p.PauseStart, PauseEnd = p.PauseEnd }).ToList();

                await _leagueService.Create(League);

                _regionManager.RequestNavigate(RegionNames.BesidesToolBarRegion, typeof(BesidesToolBarView).FullName);
            }
            else
            {
                //todo: handle cup creation
            }
        }

        //private DelegateCommand _chooseImageCommand;

        //public DelegateCommand ChooseImageCommand => _chooseImageCommand ?? (_chooseImageCommand = new DelegateCommand(ChooseImageAction));


        //public void ChooseImageAction()
        //{
        //    OpenFileDialog fileChooser = new OpenFileDialog();
        //    fileChooser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
        //    fileChooser.FilterIndex = 1;
        //    fileChooser.Multiselect = true;

        //    if (fileChooser.ShowDialog() == DialogResult.OK)
        //    {
        //        ImagePath = fileChooser.FileName;
        //    }

        //}



        #endregion

        #region Helpers
        //private byte[] GetBytesFromImage(string imagePath)
        //{
        //    if (imagePath != string.Empty)
        //    {
        //        Bitmap image = new Bitmap(imagePath);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            image.Save(ms, ImageFormat.Png);
        //            return ms.ToArray();
        //        }
        //    }
        //    return null;
        //}
        #endregion

        #region Navigation

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        #endregion
    }

    public class PauseUIModel : BindableBase
    {
        public EventHandler Updated;

        private DateTime _startPause = DateTime.Now;
        public DateTime PauseStart
        {
            get { return _startPause; }
            set
            {
                SetProperty(ref _startPause, value);
                Updated?.Invoke(this, null);
            }
        }

        private DateTime _endPause = DateTime.Now;
        public DateTime PauseEnd
        {
            get { return _endPause; }
            set
            {
                SetProperty(ref _endPause, value);
                Updated?.Invoke(this, null);
            }
        }
    }
}
