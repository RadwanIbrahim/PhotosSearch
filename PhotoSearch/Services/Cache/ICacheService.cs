using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSearch.Services.Cache
{
    public interface ICacheService
    {
        T GetData<T>(string cacheKey);
        void SaveData<T>(string cacheKey, T content);
        bool ContainsKey(string key);
        void Clear();
    }
}
