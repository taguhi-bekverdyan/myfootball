using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class DayOfWeekCheked
    {
        private DayOfWeek _dayOfWeek;
        private bool _isCheked;
        public DayOfWeek DayOfWeek
        {
            get { return _dayOfWeek; }
            set { SetProperty(ref _dayOfWeek, value); }
        }
        public bool IsCheked
        {
            get { return _isCheked; }
            set { SetProperty(ref _isCheked, value); }
        }
    }
}
