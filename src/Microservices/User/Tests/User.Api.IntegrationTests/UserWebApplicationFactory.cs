using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence;
using User.API;

namespace User.Api.IntegrationTests
{
    internal class UserWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder) {
            builder.ConfigureTestServices(services => {
                var dbService = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
                services.Remove(dbService);
                services.AddDbContext<ApplicationDbContext>(cfg => {
                    cfg.UseInMemoryDatabase("test");
                });

                var dbContext = CreateDbContext(services);
                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
            });
        }

        public async Task SetDataAsync<T>(T data) where T : class {
            var dbContext = CreateDbContext(Services);
            await dbContext.AddAsync(data);
            await dbContext.SaveChangesAsync();
        }

        public void ChangeDependency<T>(T dependency) where T : class {
            WithWebHostBuilder(builder => {
                builder.ConfigureTestServices(services => {
                    var removeService = services.SingleOrDefault(x => x.ServiceType == typeof(T));
                    services.Remove(removeService);
                    services.AddScoped(x => dependency);
                });
            });
        }

        public T GetDependency<T>() {
            return Services.CreateScope().ServiceProvider.GetRequiredService<T>();
        }

        private static ApplicationDbContext CreateDbContext(IServiceProvider serviceProvider) {
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return dbContext;
        }

        private static ApplicationDbContext CreateDbContext(IServiceCollection services) {
            var serviceProvider = services.BuildServiceProvider();
            var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            return dbContext;
        }
    }
}