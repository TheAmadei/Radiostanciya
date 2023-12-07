using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.PerformerViewModels
{
    public class FilterViewModel
    {
        public SelectList Performers { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Performer> performers, int? id, string name)
        {
            performers.Insert(0, new Models.Performer { Id = 0 });
            Performers = new SelectList(performers, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
