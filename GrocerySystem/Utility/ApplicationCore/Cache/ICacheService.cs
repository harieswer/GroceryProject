﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Cache
{
    public interface ICacheService
    {
        Task SetAsync(string key, string value, TimeSpan? expiry = null);
        Task<string?> GetAsync(string key);
        Task RemoveAsync(string key);

    }
}
