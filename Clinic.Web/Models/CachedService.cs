using Blazored.LocalStorage;
using Clinic.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Shared.Models
{
    public class CachedService<T> : ICachedService<T> where T : CachableEntity
    {
        private readonly ILocalStorageService _localStorageService;
        private ILogger<CachedService<T>> _logger;

        public string CacheKey { get; private set; }
        public TimeSpan ValidityPeriod { get; set; } = TimeSpan.FromSeconds(60);

        public CachedService(ILocalStorageService localStorageService,
            ILogger<CachedService<T>> logger)
        {
            _localStorageService = localStorageService;
            _logger = logger;
            CacheKey = nameof(T);
        }

        public async Task<T> GetById(Guid id)
        {
            return (await GetList()).FirstOrDefault(x => x.Id == id);
        }

        public async Task<List<T>> GetList()
        {
            string key = CacheKey;
            var cacheEntry = await _localStorageService.GetItemAsync<CacheEntry<List<T>>>(key);
            if (cacheEntry != null)
            {
                _logger.LogInformation("Loading items from local storage.");
                if (cacheEntry.DateCreated.Add(ValidityPeriod) > DateTime.UtcNow)
                {
                    return cacheEntry.Value;
                }
                else
                {
                    _logger.LogInformation($"Removing expired {key} from local storage.");
                    await _localStorageService.RemoveItemAsync(key);
                }
            }

            return null;
        }

        public async Task RefreshList(List<T> items)
        {
            string key = CacheKey;
            await _localStorageService.RemoveItemAsync(key);

            //var items = await _cachableService.Refresh(pageSize);
            var entry = new CacheEntry<List<T>>(items);
            await _localStorageService.SetItemAsync(key, entry);
        }

    }
}
