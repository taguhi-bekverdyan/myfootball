using MyFootballAdmin.Common.Prism;
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

namespace MyFootballAdmin.Main.Views.Pauses
{
    public class AddPauseViewModel : BindableBase, INavigationAware, IRegionManagerAware
    {
        private readonly IShellService _shellService;
        private readonly IEventAggregator _eventAggregator;

        public AddPauseViewModel(IShellService shellService, IEventAggregator eventAggregator)
        {
            _shellService = shellService;
            _eventAggregator = eventAggregator;
        }

        //private DateTime _pauseStartDate;

        //public DateTime PauseStartDate
        //{
        //    get { return _pauseStartDate; }
        //    set
        //    {
        //        SetProperty(ref _pauseStartDate, value);
        //    }
        //}

        //private DateTime _pauseEndDate;

        //public DateTime PauseEndDate
        //{
        //    get { return _pauseEndDate; }
        //    set
        //    {
        //        SetProperty(ref _pauseEndDate, value);
        //    }
        //}


        public IRegionManager RegionManager { get; set; }

        #region Navigation

        public void OnNavigatedTo(NavigationContext navigationContext)
        {

        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        #endregion

        #region Commands

        private DelegateCommand _addCommand;

        public DelegateCommand AddCommand => _addCommand ?? (_addCommand = new DelegateCommand(AddCommandAction));

        public void AddCommandAction()
        {
           
        }

        #endregion
    }

}
