using Prism.Mvvm;
using System;

namespace MyFootballAdmin.Data.Models
{
    public class Pause: BindableBase
    {
    private DateTime _startPause;
    public DateTime PauseStart
        {
        get { return _startPause; }
        set { SetProperty(ref _startPause, value); }
    }

    private DateTime _endPause;
    public DateTime PauseEnd
        {
        get { return _endPause; }
        set { SetProperty(ref _endPause, value); }
    }
}
}