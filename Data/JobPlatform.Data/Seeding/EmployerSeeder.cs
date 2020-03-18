using JobPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatform.Data.Seeding
{
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
                Location = new Location
                {
                    City = "Sofia",
                    Country = "Bulgaria",
                },
            });
        }
    }
}
