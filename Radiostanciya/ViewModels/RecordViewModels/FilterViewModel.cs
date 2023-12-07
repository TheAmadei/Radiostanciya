using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.RecordViewModels
{
    public class FilterViewModel
    {
        public SelectList Records { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Record> r, int? id, string name)
        {
            r.Insert(0, new Models.Record { Id = 0 });
            Records = new SelectList(r, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
