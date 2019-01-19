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
using System.Collections.Generic;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using MyFootballAdmin.Data.Models;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddTournamentViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INotificationService _notificationService;
        private readonly IRegionManager _regionManager;

        public AddTournamentViewModel(IShellService shellService, IEventAggregator eventAggregator, INotificationService notificationService, IRegionManager regionManager)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
            _notificationService = notificationService;
            _regionManager = regionManager;
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

        private DelegateCommand _nextCommand;
        private DelegateCommand _chooseCommand;

        public DelegateCommand NextCommand => _nextCommand ?? (_nextCommand = new DelegateCommand(NextCommandAction));
        public DelegateCommand ChooseCommand => _chooseCommand ?? (_chooseCommand = new DelegateCommand(ChooseCommandAction));

        public void NextCommandAction()
        {
            Tournament Tournament = new Tournament();
            Tournament.Name = Name;
            Tournament.Priority = Priority;
            Tournament.TournamentType = TournamentType;
            //Tournament.Logo.Link = ImagePath;
            NavigationParameters param;
            param = new NavigationParameters { { "request", Tournament} };
            MessageBox.Show(Tournament.Name);
            MessageBox.Show(Tournament.Priority.ToString());
            MessageBox.Show(Tournament.TournamentType.ToString());
           // MessageBox.Show(Tournament.Logo.Link);
            if (TournamentType.Equals(TournamentType.League))
            {
                _regionManager.RequestNavigate(RegionNames.AddTournamentRegion, typeof(AddLeagueView).FullName, param);
            }
            else
            {
                _regionManager.RequestNavigate(RegionNames.AddTournamentRegion, typeof(AddCupView).FullName, param);
            }
        }
        private void ChooseCommandAction()
        {
            //OpenFileDialog fileChooser = new OpenFileDialog();
            //fileChooser.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            //fileChooser.FilterIndex = 1;
            //fileChooser.Multiselect = true;

            //if (fileChooser.ShowDialog() == DialogResult.OK)
            //{
            //    ImagePath = fileChooser.FileName;
            //}

            MessageBox.Show("IIOJK");
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
}
