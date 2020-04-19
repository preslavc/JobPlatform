﻿namespace JobPlatform.Web
{
    using System.Reflection;

    using JobPlatform.Data;
    using JobPlatform.Data.Common;
    using JobPlatform.Data.Common.Repositories;
    using JobPlatform.Data.Models;
    using JobPlatform.Data.Repositories;
    using JobPlatform.Data.Seeding;
    using JobPlatform.Services;
    using JobPlatform.Services.Data;
    using JobPlatform.Services.Mapping;
    using JobPlatform.Services.Messaging;
    using JobPlatform.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(options =>
                {
                    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                });
            services.AddRazorPages();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services
            services.AddTransient<IEmailSender, NullMessageSender>();
            services.AddTransient<ISettingsService, SettingsService>();
            services.AddTransient<IJobPostsService, JobPostsService>();
            services.AddTransient<IEmployerService, EmployerService>();
            services.AddTransient<ITagService, TagService>();
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<IStringManipulationService, StringManipulationService>();
            services.AddTransient<ICvMessageService, CvMessageService>();
            services.AddTransient<ISlugService, SlugService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                if (env.IsDevelopment())
                {
                    dbContext.Database.Migrate();
                }

                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            //app.UseStatusCodePagesWithRedirects("/Home/HttpError?statusCode={0}");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute(
                            "reportId",
                            "Administration/Report/{id:int:min(1)}",
                            new { area="Administration", controller = "Report", action = "Id", });
                        endpoints.MapControllerRoute(
                            "report",
                            "Report/Create/{postId:int:min(1)}/{slug:required}",
                            new { controller = "Report", action = "Create", });
                        endpoints.MapControllerRoute(
                            "tagged",
                            "Browse/Tagged/{tag:required}",
                            new { controller = "Browse", action = "ByTag", });
                        endpoints.MapControllerRoute(
                            "employerDetails",
                            "Employer/{id:int:min(1)}/{slug:required}",
                            new { controller = "Employer", action = "Id", });
                        endpoints.MapControllerRoute(
                            "employerDetails",
                            "Employer/{id:int:min(1)}",
                            new { controller = "Employer", action = "Id", });
                        endpoints.MapControllerRoute(
                            "jobDetails",
                            "Job/{id:int:min(1)}/{slug:required}",
                            new { controller = "Job", action = "Id", });
                        endpoints.MapControllerRoute(
                            "jobDetails",
                            "Job/{id:int:min(1)}",
                            new { controller = "Job", action = "Id", });
                        endpoints.MapControllerRoute(
                            "areaRoute",
                            "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute(
                            "default",
                            "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
