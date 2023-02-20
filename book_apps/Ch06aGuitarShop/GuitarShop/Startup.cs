using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GuitarShop
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
        }

        // Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                // most specific route – 5 required segments
                endpoints.MapControllerRoute(
                    name: "paging_and_sorting",
                    pattern: "{controller}/{action}/{cat}/page{num}/sort-by-{sortby}");

                // specific route – 4 required segments
                endpoints.MapControllerRoute(
                    name: "paging",
                    pattern: "{controller}/{action}/{cat}/page{num}");

                // least specific route – 0 required segments
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                // endpoints.MapDefaultControllerRoute();   // another way to configure default route
            });
        }
    }
}