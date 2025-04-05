namespace word_buddy
{
    public class Startup
    {
        private IConfiguration configuration {  get; set; }

        public Startup(IConfiguration cfg) {
            configuration = cfg;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddReverseProxy()
                .LoadFromConfig(configuration.GetSection("ReverseProxy"));
            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            app.UseRouting();
            app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
            app.UseEndpoints(endpoint => {
                endpoint.MapReverseProxy();
            });
        }
    }
}
