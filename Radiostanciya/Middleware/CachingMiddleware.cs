using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Radiostanciya.Models;
using Microsoft.EntityFrameworkCore;

namespace Radiostanciya.Middleware
{
    public class CachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _cache;
        private readonly ApplContext _dbContext;

        public CachingMiddleware(RequestDelegate next, IMemoryCache cache, ApplContext dbContext)
        {
            _next = next;
            _cache = cache;
            _dbContext = dbContext;
        }

        public async Task Invoke(HttpContext context)
        {
            // Кэширование данных
            var cacheKey = $"DSdhjklsdjfuisbwuifeb{context.Request.Path}";
            if (!_cache.TryGetValue(cacheKey, out List<Record> cachedData))
            {
                // Если данные не найдены в кэше, получаем их из базы данных
                cachedData = await GetDataFromDatabase();

                // Кэшируем данные
                _cache.Set(cacheKey, cachedData, TimeSpan.FromSeconds(2 * 15 + 240));
            }

            // Выполняем следующий обработчик в конвейере
            await _next(context);
        }

        private async Task<List<Record>> GetDataFromDatabase()
        {
            var records = await _dbContext.Records
                .Include(r => r.Performer)
                .Include(r => r.Genre)
                .Include(r => r.Employee)
                // Добавьте любые дополнительные условия или сортировку при необходимости
                .ToListAsync();

            return records;
            // Пример: _dbContext.YourTable.Take(20).ToList();
        }
    }

    // Расширение для добавления middleware в конвейер
    public static class CachingMiddlewareExtensions
    {
        public static IApplicationBuilder UseCachingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CachingMiddleware>();
        }
    }
}
