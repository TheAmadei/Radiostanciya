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
using Radiostanciya.ViewModels.RecordViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class RecordsController : Controller
    {

        ApplContext db;
        public RecordsController(ApplContext db)
        {
            this.db = db;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public async Task<IActionResult> Index(int? id, string name, string per, string album, string emp, string ganre,  int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            ViewData["IsAdmin"] = User.IsInRole("Admin");

            int pageSize = 25;  // количество элементов на странице

            IQueryable<Record> source = db.Records;
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
            if (!String.IsNullOrEmpty(per))
            {
                source = source.Where(p => db.Performers.First(r => r.Id == p.PerformerId).Name.Contains(per));
            }
            if (!String.IsNullOrEmpty(album))
            {
                source = source.Where(p => p.Album.Contains(album));
            }
            if (!String.IsNullOrEmpty(emp))
            {
                source = source.Where(p => db.Employees.First(r => r.Id == p.EmployeeId).Name.Contains(emp));
            }
            if (!String.IsNullOrEmpty(ganre))
            {
                source = source.Where(p => db.Genres.First(r => r.Id == p.GenreId).Name.Contains(ganre));
            }


            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.PerformerDesc:
                    source = source.OrderByDescending(s => db.Performers.First(r => r.Id == s.PerformerId).Name);
                    break;
                case SortState.PerformerAsc:
                    source = source.OrderBy(s => db.Performers.First(r => r.Id == s.PerformerId).Name);
                    break;
                case SortState.AlbumDesc:
                    source = source.OrderByDescending(s => s.Album);
                    break;
                case SortState.AlbumAsc:
                    source = source.OrderBy(s => s.Album);
                    break;
                case SortState.GenreDesc:
                    source = source.OrderByDescending(s => db.Genres.First(r => r.Id == s.GenreId).Name);
                    break;
                case SortState.GenreAsc:
                    source = source.OrderBy(s => db.Genres.First(r => r.Id == s.GenreId).Name);
                    break;
                case SortState.DateDesc:
                    source = source.OrderByDescending(s => s.RecordDate);
                    break;
                case SortState.DateAsc:
                    source = source.OrderBy(s => s.RecordDate);
                    break;
                case SortState.EmployeeDesc:
                    source = source.OrderByDescending(s => db.Employees.First(r => r.Id == s.EmployeeId).Name);
                    break;
                case SortState.EmployeeAsc:
                    source = source.OrderBy(s => db.Employees.First(r => r.Id == s.EmployeeId).Name);
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
                FilterViewModel = new FilterViewModel(db.Records.ToList(), id, name),
                Records = items,
                Employees = db.Employees,
                Genres = db.Genres,
                Performers = db.Performers
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Insert(string name, int performerId, string album, int year, int genreId, string date, int time, int empId, int rating)
        {

            Record p = new Record
            {
                EmployeeId = empId,
                PerformerId = performerId,
                Age = year,
                Name = name,
                Album = album,
                GenreId = genreId,
                RecordDate = DateTime.Parse(date),
                TimeMin = time,
                Rating = rating
            };
            db.Records.Add(p);
            db.SaveChanges();
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Record p = null;
            try
            {
                p = db.Records.First(c => c.Id == id);
                db.Records.Remove(p);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id, string name, int performerId, string album, int year, int genreId, string date, int time, int empId, int rating)
        {
            Record p = null;
            try
            {
                p = db.Records.First(c => c.Id == id);
                p.EmployeeId = empId;
                p.PerformerId = performerId;
                p.Age = year;
                p.Name = name;
                p.Album = album;
                p.GenreId = genreId;
                p.RecordDate = DateTime.Parse(date);
                p.TimeMin = time;
                p.Rating = rating;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }
    }
}