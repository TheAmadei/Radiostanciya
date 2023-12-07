using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.ScheuduleViewModels
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState EmployeeSort { get; private set; }
        public SortState Current {
            get; private set;
        }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            EmployeeSort = sortOrder == SortState.EmployeeAsc ? SortState.EmployeeDesc : SortState.EmployeeAsc;
            Current = sortOrder;
        }
    }
}
