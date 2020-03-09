namespace JobPlatform.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using JobPlatform.Data.Models;

    public class TagSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Tags.Any())
            {
                return;
            }

            string[] initialTags = new string[]
            {
                "Web Development",
                "C#",
                "ASP.NET",
                "WPF",
            };

            foreach (var tag in initialTags)
            {
                await dbContext.Tags.AddAsync(new Tag
                {
                    Name = tag,
                });
            }
        }
    }
}
