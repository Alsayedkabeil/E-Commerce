using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Abstractions.Chaching
{
    public interface IChachingServices
    {
        Task SetChacheValueAsync(string key, object value, TimeSpan time);
        Task<string?> GetChacheValueAsync(string key);
    }
}
