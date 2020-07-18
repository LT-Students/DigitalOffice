using System;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using CheckRightsService.Database;

using MassTransit;
using GreenPipes;

namespace CheckRightsService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IBusControl CreateBus(IServiceProvider serviceProvider)
        {
            const string serviceInfoSection = "ServiceInfo";

            var serviceName = Configuration.GetSection(serviceInfoSection)["Name"] ?? "CheckRightsService";
            var serviceId = Configuration.GetSection(serviceInfoSection)["Id"] ?? Guid.NewGuid().ToString();

            return Bus.Factory.CreateUsingRabbitMq(config =>
            {
                config.Host("localhost", "/", host =>
                {
                    host.Username($"{serviceName}_{serviceId}");
                    host.Password($"{serviceId}");
                });

                config.ReceiveEndpoint($"{serviceName}", endpoint =>
                {
                    endpoint.PrefetchCount = 16;
                    endpoint.UseMessageRetry(retry => retry.Interval(2, 100));
                });
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = scope.ServiceProvider.GetService<CheckRightsServiceDbContext>();

            context.Database.Migrate();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();

            services.AddDbContext<CheckRightsServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddMassTransit(config =>
            {
                config.AddBus(provider => CreateBus(provider));
            });

            services.AddMassTransitHostedService();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
