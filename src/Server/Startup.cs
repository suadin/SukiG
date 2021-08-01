using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SukiG.Server.Database;
using SukiG.Server.Hubs;

namespace SukiG.Server
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddSignalR();

            services.AddSingleton<IConfiguration>(configuration);
            services.AddHttpClient();

            services.AddDbContext<DefaultDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddAuthentication()
                .AddGoogle(o =>
                {
                    o.ClientId = configuration["Authentication:Google:ClientId"];
                    o.ClientSecret = configuration["Authentication:Google:ClientSecret"];
                });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();

                logger.LogInformation($"ClientId={configuration["Authentication:Google:ClientId"]}");
                logger.LogInformation($"ClientSecret={configuration["Authentication:Google:ClientSecret"]}");
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            logger.LogInformation($"Database migration...");
            app.Migrate();
            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
                endpoints.MapHub<ChatServerHub>(ChatServerHub.HubUrl);
            });
        }
    }
}