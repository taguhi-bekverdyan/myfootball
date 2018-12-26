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

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddTournamentViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;

        public AddTournamentViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
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

        private int _teamsCount;

        public int TeamsCount
        {
            get { return _teamsCount; }
            set
            {
                if (_teamsCount > 48)
                { }
                else
                { SetProperty(ref _teamsCount, value); }
            }
        }


        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            { 
                SetProperty(ref _endDate, value);
            }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set { SetProperty(ref _startDate, value);}
        }

        private int _rounds;

        public int Rounds
        {
            get { return _rounds; }
            set { SetProperty(ref _rounds, value); }
        }

        private string _imagePath;

        public string ImagePath
        {
            get { return _imagePath; }
            set { SetProperty(ref _imagePath, value); }
        }

        private TournamentType _tournamentType;

        public TournamentType TournamentType
        {
            get { return _tournamentType; }
            set { SetProperty(ref _tournamentType, value); }
        }
        #endregion


        public IRegionManager RegionManager { get; set; }

        #region Commands

        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandAction));


        public void AddCommandAction()
        {
            CreateTournament();
            _notificationService.ShowNotification(NotificationType.Alert, "Tournament is sussecfully added!");
        }

        private DelegateCommand _chooseImageCommand;

        public DelegateCommand ChooseImageCommand => _chooseImageCommand ?? (_chooseImageCommand = new DelegateCommand(ChooseImageAction));


        public void ChooseImageAction()
        {
            OpenFileDialog fileChooser = new OpenFileDialog();
            fileChooser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            fileChooser.FilterIndex = 1;
            fileChooser.Multiselect = true;

            if (fileChooser.ShowDialog() == DialogResult.OK)
            {
                ImagePath = fileChooser.FileName;
            }

        }



        #endregion

        #region Helpers
        private byte[] GetBytesFromImage(string imagePath)
        {
            if (imagePath != string.Empty)
            {
                Bitmap image = new Bitmap(imagePath);
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
            return null;
        }

        public void CreateTournament()
        {
            Tournament Tournament = new Tournament();
            Tournament.teamsCount = TeamsCount;
            Tournament.name = Name;
            Tournament.tournamentType = TournamentType;
            Tournament.rounds = Rounds;
            Tournament.startDate = StartDate;
            Tournament.endDate = EndDate;
            Tournament.image = GetBytesFromImage(ImagePath);
            Tournaments.Add(Tournament);
        }
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
}
