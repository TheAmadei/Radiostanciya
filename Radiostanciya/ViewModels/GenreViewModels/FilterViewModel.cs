using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Radiostanciya.ViewModels.GenreViewModels
{
    public class FilterViewModel
    {
        public SelectList Genres { get; private set; }
        public int? SelectedId { get; private set; }
        public string SelectedName { get; private set; }

        public FilterViewModel(List<Models.Genre> genres, int? id, string name)
        {
            genres.Insert(0, new Models.Genre { Id = 0 });
            Genres = new SelectList(genres, "Id", name, id);
            SelectedId = id;
            SelectedName = name;
        }
    }
}
