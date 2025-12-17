using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Domain.Contracts
{
    // This is a placeholder interface for caching repository functionality
    // Actual methods and properties should be defined based on caching requirements
    public interface IChachingRepostory
    {
        Task SetAsync(string key,object value,TimeSpan time); // Store data in cache with expiration time
        Task<string?> GetAsync(string key); // Retrieve data from cache by key
    }
}
