namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;

    public interface IApplicationUserService
    {
        int GetUserCount();

        IEnumerable<T> GetUsersByName<T>(string name);
    }
}
