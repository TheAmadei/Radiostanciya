using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.ScheuduleViewModels
{
    public class FilterViewModel
    {
        public SelectList Scheudules { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Schedule> sc, int? id, string name)
        {
            sc.Insert(0, new Models.Schedule { Id = 0 });
            Scheudules = new SelectList(sc, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
