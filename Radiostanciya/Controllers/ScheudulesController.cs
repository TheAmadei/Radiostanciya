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
using Radiostanciya.ViewModels.ScheuduleViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class ScheudulesController : Controller
    {

        ApplContext db;
        public ScheudulesController(ApplContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            ViewData["IsAdmin"] = User.IsInRole("Admin");

            int pageSize = 10;  // количество элементов на странице

            IQueryable<Schedule> source = db.Schedules;
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
                source = source.Where(p => db.Employees.First(r => r.Id == p.EmployeeId).Name.Contains(name));
            }

            switch (sortOrder)
            {
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
                FilterViewModel = new FilterViewModel(db.Schedules.ToList(), id, name),
                Schedules = items,
                Employees = db.Employees
            };
            return View(viewModel);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Insert(int empId, string date, string start, string end)
        {
            Console.WriteLine($"Insert called with empId: {empId}, date: {date}, start: {start}, end: {end}");
            Schedule p = new Schedule
            {
                EmployeeId = empId,
                Date = DateTime.Parse(date),
                Start = DateTime.Parse(start),
                End = DateTime.Parse(end)
            };
            db.Schedules.Add(p);
            db.SaveChanges();
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int id)
        {
            Schedule p = null;
            try
            {
                p = db.Schedules.First(c => c.Id == id);
                db.Schedules.Remove(p);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult Update(int id, int empId, string date, string start, string end)
        {
            Schedule p = null;
            try
            {
                p = db.Schedules.First(c => c.Id == id);
                p.EmployeeId = empId;
                p.Date = DateTime.Parse(date);
                p.Start = DateTime.Parse(start);
                p.End = DateTime.Parse(end);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }
    }
}