using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostanciya.Extensions;
using Radiostanciya.Models;
using Radiostanciya.ViewModels.DashboardViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ApplContext db;

        public HomeController(ApplContext db)
        {
            this.db = db;
        }
        public IActionResult Index(int? offset)
        {
            offset ??= 0;

            DateTime today = DateTime.Now.AddDays(offset.Value);
            DateTime yesterday = today.AddDays(-1);
            DateTime tomorrow = today.AddDays(1);

            var yesterdayTranslations = db.Schedules
                .Include(s => s.Employee)
                .Include(s => s.Record)
                .Where(t => t.Date != null && t.Date.Date == yesterday.Date)
                .ToList();

            var todayTranslations = db.Schedules
                .Include(s => s.Employee)
                .Include(s => s.Record)
                .Where(t => t.Date != null && t.Date.Date == today.Date)
                .ToList();

            var tomorrowTranslations = db.Schedules
                .Include(s => s.Employee)
                .Include(s => s.Record)
                .Where(t => t.Date != null && t.Date.Date == tomorrow.Date)
                .ToList();

            var viewModel = new DashboardViewModel
            {
                YesterdayDate = yesterday,
                Yesterday = yesterdayTranslations,
                TodayDate = today,
                Today = todayTranslations,
                TomorrowDate = tomorrow,
                Tomorrow = tomorrowTranslations
            };

            return View(viewModel);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
