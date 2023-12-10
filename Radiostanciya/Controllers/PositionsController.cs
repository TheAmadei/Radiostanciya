using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostanciya.Migrations;
using Radiostanciya.Models;
using Radiostanciya.ViewModels;
using Radiostanciya.ViewModels.PositionViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PositionsController : Controller
    {

        ApplContext db;
        public PositionsController(ApplContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            int pageSize = 10;  // количество элементов на странице

            IQueryable<Position> source = db.Positions;
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
                case SortState.SalaryDesc:
                    source = source.OrderByDescending(s => s.Salary);
                    break;
                case SortState.SalaryAsc:
                    source = source.OrderBy(s => s.Salary);
                    break;
                case SortState.RequirementsDesc:
                    source = source.OrderByDescending(s => s.Requirements);
                    break;
                case SortState.RequirementsAsc:
                    source = source.OrderBy(s => s.Requirements);
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
                FilterViewModel = new FilterViewModel(db.Positions.ToList(), id, name),
                Positions = items
            };
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Insert(string name, string sal, string res, string req)
        {
            Position p = new Position
            {
                Name = name,
                Salary = Convert.ToDouble(sal),
                Requirements = req,
                Responsibilities = res
            };
            db.Positions.Add(p);
            db.SaveChanges();
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Position p = null;
            try
            {
                p = db.Positions.Where(c => c.Id == id).First();
                db.Positions.Remove(p);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string sal, string res, string req)
        {
            Position p = null;
            try
            {
                p = db.Positions.Where(c => c.Id == id).First();
                p.Name = name;
                p.Salary = Convert.ToDouble(sal);
                p.Responsibilities = res;
                p.Requirements = req;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }
    }
}