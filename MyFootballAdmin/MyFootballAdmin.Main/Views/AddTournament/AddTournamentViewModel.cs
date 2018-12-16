using MyFootballAdmin.Common;
using MyFootballAdmin.Common.Prism;
using MyFootballAdmin.Main.Views.Main;
using MyFootballAdmin.Main.Views.Notifications;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballAdmin.Main.Views.AddTournament
{
    public class AddTournamentViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {

        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public AddTournamentViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
        }

        #region Types

        private Tournament _tournament;

        public Tournament Tournament
        {
            get { return _tournament; }
            set { SetProperty(ref _tournament, value); }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _maxCount;

        public int MaxCount
        {
            get { return _maxCount; }
            set
            {
                if (_maxCount > 48)
                { }
                else
                { SetProperty(ref _maxCount, value); }
            }
        }

        private int _minCount ;

        public int MinCount
        {
            get { return _minCount; }
            set
            {
                if (_minCount < 4)
                { }
                else
                { SetProperty(ref _minCount, value); }
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get { return _endDate; }
            set {
                if (_endDate < new DateTime(01 / 01 / 2019))
                {
                    _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs { Notification = new Notifications.Notification(NotificationType.Error,"Date can not be till 2019.") });
                }
                else
                {
                    SetProperty(ref _endDate, value);
                }
                }
        }

        private DateTime _startDate;

        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate < new DateTime(01 / 01 / 2019))
                {
                    _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs { Notification = new Notifications.Notification(NotificationType.Error, "Date can not be till 2019.") });
                }
                else
                {
                    SetProperty(ref _startDate, value);
                }
            }
        }

        private ResponseMatch _responseMatch;

        public ResponseMatch ResponseMatch
        {
            get { return _responseMatch; }
            set { SetProperty(ref _responseMatch, value); }
        }

        private TournamentType _tournamentType;

        public TournamentType TournamentType
        {
            get { return _tournamentType; }
            set { SetProperty(ref _tournamentType, value); }
        }
        #endregion

        #region Commands

        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandAction));

        public IRegionManager RegionManager { get; set; }

        public void AddCommandAction()
        {
            CreateTournament();
        }

        #endregion

        public void CreateTournament()
        {
            Tournament Tournament = new Tournament();
            Tournament.teamsMaxCount = MaxCount;
            Tournament.teamsMinCount = MinCount;
            Tournament.name = Name;
            Tournament.tournamentType = TournamentType;
            Tournament.responseMatch = ResponseMatch;
            Tournament.startDate = StartDate;
            Tournament.endDate = EndDate;
            RegionManager.RequestNavigate(RegionNames.WindowContentRegion, nameof(MainView));
            _eventAggregator.GetEvent<NotificationEvent>().Publish(new NotificationEventArgs { Notification = new Notifications.Notification(NotificationType.Alert, "Tournament successfully added!") });
        }

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

        #region Event

        public class NotificationEvent : PubSubEvent<NotificationEventArgs> { }

        public class NotificationEventArgs
        {
            public Notification Notification { get; set; }
        }

        #endregion
    }
}
