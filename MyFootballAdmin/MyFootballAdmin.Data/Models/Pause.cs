using System;
using Prism.Mvvm;

namespace MyFootballAdmin.Data.Models
{
    public class Pause : BindableBase
    {
        public EventHandler Updated;

        private DateTime _startPause;
        public DateTime PauseStart
            {
            get { return _startPause; }
            set
            {
                SetProperty(ref _startPause, value);
                Updated?.Invoke(this, null);
            }
        }

        private DateTime _endPause;
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