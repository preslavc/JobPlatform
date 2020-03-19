using JobPlatform.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPlatform.Data.Seeding
{
    public class JobPostSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.JobPosts.Any())
            {
                return;
            }

            List<JobPost> initialPosts = new List<JobPost>
            {
                new JobPost
                {
                    Title = "Software Developer",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    City = "Sofia",
                    Country = "Bulgaria",
                    EmployerId = 1,
                },
                new JobPost
                {
                    Title = "Web Developer",
                    Description = "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                    City = "Pleven",
                    Country = "Bulgaria",
                    EmployerId = 1,
                },
            };

            foreach (var post in initialPosts)
            {
                await dbContext.JobPosts.AddAsync(post);
            }
        }
    }
}
