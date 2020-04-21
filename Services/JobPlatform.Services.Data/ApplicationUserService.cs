namespace JobPlatform.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Services.Mapping;

    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public ApplicationUserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public int GetUserCount()
        {
            return this.userRepository.All().Count();
        }

        public IEnumerable<T> GetUsersByName<T>(string name)
        {
            return this.userRepository.All()
                .Where(x => x.Email.Contains(name) || x.UserName.Contains(name))
                .To<T>()
                .ToList();
        }
    }
}
