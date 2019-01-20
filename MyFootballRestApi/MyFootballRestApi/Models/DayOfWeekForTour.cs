using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFootballRestApi.Models
{
    public class DayOfWeekForTour
    {
        public List<DateTime> TourWeekDays { get; set; }
        public DayOfWeekForTour()
        {
            TourWeekDays = new List<DateTime>();
        }
      
    }
}
