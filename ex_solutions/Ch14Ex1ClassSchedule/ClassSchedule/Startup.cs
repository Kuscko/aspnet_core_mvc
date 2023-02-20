using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using ClassSchedule.Models;

namespace ClassSchedule
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMemoryCache();
            services.AddSession();

            services.AddTransient<IClassScheduleUnitOfWork, ClassScheduleUnitOfWork>();
            services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
            services.AddHttpContextAccessor();

            services.AddDbContext<ClassScheduleContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("ClassScheduleContext")));
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
