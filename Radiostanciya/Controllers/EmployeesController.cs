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
using Radiostanciya.ViewModels.Account.Register;
using Radiostanciya.ViewModels.EmployeeViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize(Roles = "Admin")]
    public class EmployeesController : Controller
    {

        ApplContext db;
        public EmployeesController(ApplContext db)
        {
            this.db = db;
        }

        public async Task<IActionResult> Index(int? id, string name, string pos, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            ViewData["IsAdmin"] = User.IsInRole("Admin");

            int pageSize = 10;  // количество элементов на странице

            IQueryable<Employee> source = db.Employees;
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
            if (!String.IsNullOrEmpty(pos))
            {
                source = source.Where(p => db.Positions.First(r=> r.Id == p.PositionId).Name.Contains(pos));
            }

            switch (sortOrder)
            {
                case SortState.NameDesc:
                    source = source.OrderByDescending(s => s.Name);
                    break;
                case SortState.NameAsc:
                    source = source.OrderBy(s => s.Name);
                    break;
                case SortState.PositionDesc:
                    source = source.OrderByDescending(s => s.Position.Name);
                    break;
                case SortState.PositionAsc:
                    source = source.OrderBy(s => s.Position.Name);
                    break;
                case SortState.AgeDesc:
                    source = source.OrderByDescending(s => s.Age);
                    break;
                case SortState.AgeAsc:
                    source = source.OrderBy(s => s.Age);
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
                FilterViewModel = new FilterViewModel(db.Employees.ToList(), id, name),
                Employees = items,
                Positions = db.Positions
            };


            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Insert(string name, int age, string sex, string address, string passport, int posId)
        {
            Employee p = new Employee
            {
                Name = name,
                Age = age,
                Sex = sex,
                Address = address,
                Passport = passport,
                PositionId =posId
            };
            db.Employees.Add(p);
            db.SaveChanges();
            JsonResult data = Json(p);
            return data;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Employee p = null;
            try
            {
                p = db.Employees.Where(c => c.Id == id).First();
                db.Employees.Remove(p);
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Update(int id, string name, string age, string sex, string address, string passport, int posId)
        {
            Employee p = null;
            try
            {
                p = db.Employees.Where(c => c.Id == id).First();
                p.Name = name;
                p.Age = Convert.ToInt32(age);
                p.Sex = sex;
                p.Address = address;
                p.Passport = passport;
                p.PositionId = posId;
                db.SaveChanges();
            }
            catch { }
            JsonResult data = Json(p);
            return data;
        }
    }
}