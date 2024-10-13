using Microsoft.EntityFrameworkCore;
using Persistence;

namespace word_buddy
{
    public class Startup
    {
        private IConfiguration configuration {  get; set; }

        public Startup(IConfiguration cfg) {
            configuration = cfg;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllersWithViews();

            services.AddDbContext<ApplicationDbContext>(cfg => {
                cfg.UseNpgsql(configuration["ConnectionStrings:DatabaseConnection"]);
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (!env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
