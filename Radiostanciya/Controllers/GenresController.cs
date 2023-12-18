using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Radiostanciya.Models;
using Microsoft.Extensions.Caching.Memory;
using Radiostanciya.ViewModels;
using Radiostanciya.ViewModels.GenreViewModels;

namespace Radiostanciya.Controllers
{
    [Authorize]
    public class GenresController : Controller
    {
        private readonly ApplContext db;
        private readonly IMemoryCache memoryCache;
        public GenresController(ApplContext db, IMemoryCache memoryCache)
        {
            this.db = db;
            this.memoryCache = memoryCache;
        }

        [ResponseCache(CacheProfileName = "Caching")]
        public async Task<IActionResult> Index(int? id, string name, int page = 0,
            SortState sortOrder = SortState.IdAsc)
        {
            // Проверяем, есть ли данные в кэше
            if (!memoryCache.TryGetValue("GenresList", out List<Genre> genres))
            {
                // Данные отсутствуют в кэше, извлекаем из базы данных
                genres = await db.Genres.ToListAsync();

                // Кэшируем данные
                memoryCache.Set("GenresList", genres, new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10), // Кэшировать на 10 минут
                    SlidingExpiration = TimeSpan.FromMinutes(2) // Обновлять, если не использовалось более 2 минут
                });
            }

            ViewData["IsAdmin"] = User.IsInRole("Admin");

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

            /*// Вывести данные из кэша в лог
            if (memoryCache.TryGetValue("GenresList", out List<Genre> cachedGenres))
            {
                foreach (var genre in cachedGenres)
                {
                    Console.WriteLine($"Cached Genre: {genre.Id} - {genre.Name}");
                }
            }*/
            return await Task.FromResult(View(viewModel));
        }

        [HttpGet]
        public ActionResult Insert(string name, string description)
        {
            // Обновляем кэш при вставке новых данных
            memoryCache.Remove("GenresList");

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
            // Обновляем кэш при удалении данных
            memoryCache.Remove("GenresList");

            Genre genre = null;
            try
            {
                genre = db.Genres.Where(c => c.Id == id).FirstOrDefault();
                if (genre != null)
                {
                    db.Genres.Remove(genre);
                    db.SaveChanges();
                }
            }
            catch { }

            JsonResult data = Json(genre);
            return data;
        }

        [HttpGet]
        public ActionResult Update(int id, string name, string desc)
        {
            // Обновляем кэш при обновлении данных
            memoryCache.Remove("GenresList");

            Genre genre = null;
            try
            {
                genre = db.Genres.Where(c => c.Id == id).FirstOrDefault();
                if (genre != null)
                {
                    genre.Name = name;
                    genre.Description = desc;
                    db.SaveChanges();
                }
            }
            catch { }

            JsonResult data = Json(desc);
            return data;
        }
    }
}