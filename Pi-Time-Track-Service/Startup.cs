using System;
using Adapters.Database;
using Adapters.Database.Install;
using Adapters.Hangfire;
using Adapters.Hangfire.Interfaces;
using Application;
using Hangfire;
using Hangfire.SQLite;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Pi_Time_Track_Service
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            //Application
            services.AddApplicationServices();

            //Database
            services.AddDatabaseServices();

            //Hangfire
            const string dbPath = "Data Source=D:\\SQLite\\DBs\\PiTimeTrackService.db;";
            services.AddHangfire(x => x.UseSQLiteStorage(dbPath));
            services.AddHangfireServer();
            services.AddHangfireServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHangfireStartup hangfireStartup, IDbSetup dbSetup)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });

            app.UseHangfireDashboard();
            dbSetup.Install();
            hangfireStartup.AddHangfireJobs();
        }
    }
}
