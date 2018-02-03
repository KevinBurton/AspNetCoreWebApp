using System;
using System.Linq;
using System.Collections.Generic;

namespace AspNetCoreWebApp.Repositories.Interfaces
{
    public interface ICacheRepository
    {
        Dictionary<string, List<KeyValuePair<string, string>>> Info(string section);
    }
}
