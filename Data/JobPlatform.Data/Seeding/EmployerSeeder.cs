namespace JobPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;

    public class EmployerSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Employers.Any())
            {
                return;
            }

            await dbContext.Employers.AddAsync(new Employer
            {
                Name = "FirmBG",
                Eik = "000000000",
                City = "Sofia",
                Country = "Bulgaria",
                ImageUrl = "https://softuni.bg/companies/profile/logo/134",
            });
        }
    }
}
