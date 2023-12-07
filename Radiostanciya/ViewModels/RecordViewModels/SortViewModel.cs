using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.RecordViewModels
{
    public class SortViewModel
    {
        public SortState IdSort { get; private set; }
        public SortState NameSort { get; private set; }
        public SortState PerformerSort { get; private set; }
        public SortState AlbumSort { get; private set; }
        public SortState GenreSort { get; private set; }
        public SortState DateSort { get; private set; }
        public SortState EmployeeSort { get; private set; }
        public SortState Current {
            get; private set;
        }

        public SortViewModel(SortState sortOrder)
        {
            IdSort = sortOrder == SortState.IdAsc ? SortState.IdDesc : SortState.IdAsc;
            NameSort = sortOrder == SortState.NameAsc ? SortState.NameDesc : SortState.NameAsc;
            PerformerSort = sortOrder == SortState.PerformerAsc ? SortState.PerformerDesc : SortState.PerformerAsc;
            AlbumSort = sortOrder == SortState.AlbumAsc ? SortState.AlbumDesc : SortState.AlbumAsc;
            GenreSort = sortOrder == SortState.GenreAsc ? SortState.GenreDesc : SortState.GenreAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            EmployeeSort = sortOrder == SortState.EmployeeAsc ? SortState.EmployeeDesc : SortState.EmployeeAsc;
            Current = sortOrder;
        }
    }
}
