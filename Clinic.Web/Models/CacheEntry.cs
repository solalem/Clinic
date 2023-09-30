using System;

namespace Clinic.Web.Shared.Models
{
    public class CacheEntry<T>
    {
        public CacheEntry(T item)
        {
            Value = item;
        }
        public CacheEntry()
        {

        }

        public T Value { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
    }
}
