using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.PerformerViewModels
{
    public class IndexViewModel
    {
            public IEnumerable<Models.Performer> Performers { get; set; }
            public PageViewModel PageViewModel { get; set; }
            public FilterViewModel FilterViewModel { get; set; }
            public SortViewModel SortViewModel { get; set; }
    }
}
