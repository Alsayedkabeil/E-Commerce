using Store.Domain.Contracts;
using Store.Services.Abstractions.Chaching;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Services.Chaching
{
    public class ChachingServices(IChachingRepostory chachingRepostory) : IChachingServices
    {
        public async Task<string?> GetChacheValueAsync(string key)
        {
            var value = await chachingRepostory.GetAsync(key); // get the value from the repository
            return value == null ? null : value; // return null if value is null, otherwise return the value
        }

        public async Task SetChacheValueAsync(string key, object value, TimeSpan time)
        {
            await chachingRepostory.SetAsync(key, value, time); // set the value in the repository
        }
    }
}
