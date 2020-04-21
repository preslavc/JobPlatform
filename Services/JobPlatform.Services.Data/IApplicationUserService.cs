using JobPlatform.Data.Models;
using System.Collections.Generic;

namespace JobPlatform.Services.Data
{
    public interface IApplicationUserService
    {
        int GetUserCount();

        IEnumerable<T> GetUsersByName<T>(string name);
    }
}
