using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.PositionViewModels
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState SalarySort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState ResponsibilitiesSort { get; private set; }
        public SortState RequirementsSort { get; private set; }
        public SortState Current {
            get; private set;
        }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            SalarySort = sortOrder == SortState.SalaryAsc ? SortState.SalaryDesc : SortState.SalaryAsc;
            ResponsibilitiesSort = sortOrder == SortState.ResponsibilitiesDesc ? SortState.ResponsibilitiesDesc : SortState.ResponsibilitiesAsc;
            RequirementsSort = sortOrder == SortState.RequirementsAsc ? SortState.RequirementsDesc : SortState.RequirementsAsc;
            Current = sortOrder;
        }
    }
}
