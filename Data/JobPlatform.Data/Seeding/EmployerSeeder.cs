namespace JobPlatform.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using JobPlatform.Common;
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
                ImageUrl = GlobalConstants.DefaultEmployerImage,
                Description = "<p>Астреа Рикрутмънт е създадена през 2007 година с една основна мисия – да бъдем различни. Превърнахме това виждане в основен двигател на нашите усилия да сме винаги на разположение, когато клиентите и партньорите ни имат нужда от нас, за да ги консултираме и напътстваме при вземането на важни решения.Започнахме с офис в София и след няколко години на професионализъм успяхме да се разширим и във Велико Търново.Нашите партньори се увеличават всеки ден и тяхното удовлетворение е най - добрата визитна картичка за нас.</p>",
            });
        }
    }
}
