using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostanciya.Models;
using Radiostanciya.ViewModels;
using Radiostanciya.ViewModels.GenreViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        ApplContext db;
        public GenresController(ApplContext db)
        {
            this.db = db;
        }

        [ResponseCache(Duration = 2 * 15 + 240, Location = ResponseCacheLocation.Any)]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Genre> source = db.Genres;
            if (page == 0)
            {
                page = 1;
            }
            if (id != null && id != 0)
            {
                source = source.Where(p => p.Id == id);
            }
            if (!String.IsNullOrEmpty(name))
            {
                source = source.Where(p => p.Name.Contains(name));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.IdDesc:
                    source = source.OrderByDescending(s => s.Id);
                    break;
                default:
                    source = source.OrderBy(s => s.Id);
                    break;
            }

            var count = await source.CountAsync();
            var items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                SortViewModel = new SortViewModel(sortOrder),
                FilterViewModel = new FilterViewModel(db.Genres.ToList(), id, name),
                Genres = items
            };

            if (id.HasValue)
            {
                // Update localStorage with the selected record's ID
                HttpContext.Response.Cookies.Append("selectedId", id.Value.ToString());
            }

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(string name, string description)
        {
            Genre genre = new Genre
            {
                Name = name,
                Description = description
            };
            db.Genres.Add(genre);
            db.SaveChanges();
            JsonResult data = Json(genre);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Genre genre = null;
            try
            {
                genre = db.Genres.Where(c => c.Id == id).First();
                db.Genres.Remove(genre);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(genre);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string desc)
        {
            Genre genre = null;
            try
            {
                genre = db.Genres.Where(c => c.Id == id).First();
                genre.Name = name;
                genre.Description = desc;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(desc);
            return data;
        }
    }
}