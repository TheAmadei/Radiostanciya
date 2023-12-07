using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.PositionViewModels
{
    public class FilterViewModel
    {
        public SelectList Positions { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Position> positions, int? id, string name)
        {
            positions.Insert(0, new Models.Position { Id = 0 });
            Positions = new SelectList(positions, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
