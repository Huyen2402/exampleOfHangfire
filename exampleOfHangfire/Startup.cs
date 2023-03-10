using exampleOfHangfire.Models;
using Hangfire;
using Microsoft.Owin;
using Owin;
using System;

[assembly: OwinStartupAttribute(typeof(exampleOfHangfire.Startup))]
namespace exampleOfHangfire
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            GlobalConfiguration.Configuration.UseSqlServerStorage("DefaultConnection");
            app.UseHangfireDashboard("/myJobDashboard", new DashboardOptions() {
                Authorization = new[] { new HangfireAthorizationFilter()}
            });
            //BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            RecurringJob.AddOrUpdate(() => Console.WriteLine("Recurring!"),Cron.Minutely);
            app.UseHangfireServer();
        }
    }
}
