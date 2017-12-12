using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Services.Cache
{
    public class MemoryCacheService : ICacheService
    {
        private static readonly Dictionary<string, object> CachedData = new Dictionary<string, object>();

        public void Clear()
        {
            CachedData.Clear();
        }

        public T GetData<T>(string cacheKey)
        {
            lock (CachedData)
            {
                if (CachedData.ContainsKey(cacheKey))
                {
                    return (T)CachedData[cacheKey];
                }
                else
                    return default(T);
            }
        }
        public bool ContainsKey(string key)
        {
            lock (CachedData)
            {
                bool haskey = CachedData.ContainsKey(key);
                return haskey;
            }

        }

        public void SaveData<T>(string cacheKey, T content)
        {
            lock (CachedData)
                CachedData[cacheKey] = content;
        }
    }
}
