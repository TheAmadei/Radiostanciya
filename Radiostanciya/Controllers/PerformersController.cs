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
using Radiostanciya.ViewModels.PerformerViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class PerformersController : Controller
    {

        ApplContext db;
        public PerformersController(ApplContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Performer> source = db.Performers;
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
                FilterViewModel = new FilterViewModel(db.Performers.ToList(), id, name),
                Performers = items
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(string name, string description)
        {
            Performer p = new Performer
            {
                Name = name,
                Description = description
            };
            db.Performers.Add(p);
            db.SaveChanges();
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Performer p = null;
            try
            {
                p = db.Performers.Where(c => c.Id == id).First();
                db.Performers.Remove(p);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string desc)
        {
            Performer p = null;
            try
            {
                p = db.Performers.Where(c => c.Id == id).First();
                p.Name = name;
                p.Description = desc;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(desc);
            return data;
        }
    }
}