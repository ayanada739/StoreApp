using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.G04.Core.Sevices.Contract
{
    public interface ICacheService
    {
        Task SetCatcheKeyAsync(string key, object response, TimeSpan expireTime);
        Task<string> GetCacheKeyAsync(string key);

    }
}
