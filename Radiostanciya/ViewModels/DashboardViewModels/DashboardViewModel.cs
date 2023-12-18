using System.Collections.Generic;
using System;
using Radiostanciya.Models;

namespace Radiostanciya.ViewModels.DashboardViewModels
{
    public class DashboardViewModel
    {
        public DateTime YesterdayDate { get; set; }

        public List<Schedule> Yesterday { get; set; }

        public DateTime TodayDate { get; set; }

        public List<Schedule> Today { get; set; }

        public DateTime TomorrowDate { get; set; }

        public List<Schedule> Tomorrow { get; set; }
    }
}
