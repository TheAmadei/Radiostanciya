using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.ScheuduleViewModels
{
    public class IndexViewModel
    {
            public IEnumerable<Models.Schedule> Schedules{ get; set; }
            public IEnumerable<Models.Employee> Employees { get; set; }
            public IEnumerable<Models.Record> Records { get; set; }
            public PageViewModel PageViewModel { get; set; }
            public FilterViewModel FilterViewModel { get; set; }
            public SortViewModel SortViewModel { get; set; }
    }
}
