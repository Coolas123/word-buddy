using Application;
using Application.DomainServices;
using Application.HelpClasses;
using Domain.EntityServices;
using Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence;
using Persistence.Repositories;
using System.Text;

namespace User.API
{
    public class Startup
    {
        private IConfiguration configuration { get; set; }

        public Startup(IConfiguration cfg) {
            configuration = cfg;
        }

        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers()
                .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);

            services.AddDbContext<ApplicationDbContext>(cfg => {
                cfg.UseNpgsql(configuration["ConnectionStrings:DatabaseConnection"]);
            });

            services.AddHsts(opt => {
                opt.MaxAge = TimeSpan.FromDays(60);
            });

            services.AddAuthentication(cfg => {
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg => {
                    cfg.TokenValidationParameters = new()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"])),
                        ClockSkew = TimeSpan.Zero,
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    cfg.RequireHttpsMetadata = true;
                    cfg.SaveToken = true;
                });
            services.AddAuthorization();

            services.AddMediatR(cfg => {
                cfg.RegisterServicesFromAssembly(typeof(Application.AssemblyReference).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly,
                includeInternalTypes: true);
            
            services.AddSwaggerGen(cfg => {
                cfg.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User api"
                });

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description="Авторизация на основе JWT токена",
                    Name="Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type=ReferenceType.SecurityScheme,
                            Id="Bearer"
                        },
                        Scheme="oauth2",
                        Name="Bearer",
                        In=ParameterLocation.Header,
                    },
                    new List<string>()}
                });

                var xmlFilename = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
                cfg.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddSingleton<JWTGenerator>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IEmailUniqueCheck, EmailUniqueCheck>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseHsts();
            app.UseStaticFiles();
            app.UseCookiePolicy(new CookiePolicyOptions
            {
                HttpOnly = HttpOnlyPolicy.Always,
                Secure = CookieSecurePolicy.Always,
                MinimumSameSitePolicy = SameSiteMode.Strict

            });

            app.UseSwagger();
            app.UseSwaggerUI(cfg => {
                cfg.RoutePrefix = "swagger";
                cfg.SwaggerEndpoint("v1/swagger.json", "User Api");
            });

            app.Use(async (context, next) => {
                if (context.Request.Cookies.TryGetValue("token", out string? token))
                    context.Request.Headers.Authorization = $"Bearer {token}";
                await next();
            });
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapDefaultControllerRoute();
            });

            SeedData.Fill(app);
        }
    }
}
