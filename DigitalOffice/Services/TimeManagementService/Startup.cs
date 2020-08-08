using System;
using LT.DigitalOffice.Broker.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LT.DigitalOffice.TimeManagementService.Database;
using MassTransit;

namespace LT.DigitalOffice.TimeManagementService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<TimeManagementDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddControllers();
            
            services.AddMassTransit(configurator =>
            {
                configurator.AddRequestClient<ICheckIfUserHaveRightRequest>(
                    new Uri("rabbitmq://localhost/checkrightsservice"));

                configurator.UsingRabbitMq((context, factoryConfigurator) =>
                {
                    const string serviceInfoSection = "ServiceInfo";

                    var serviceName = Configuration.GetSection(serviceInfoSection)["Name"];
                    var serviceId = Configuration.GetSection(serviceInfoSection)["Id"];
                    
                    factoryConfigurator.Host("localhost", hostConfigurator =>
                    {
                        hostConfigurator.Username($"{serviceName}_{serviceId}");
                        hostConfigurator.Password(serviceId);
                    });
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();
            
            using var context = serviceScope.ServiceProvider.GetService<TimeManagementDbContext>();
            
            context.Database.Migrate();
        }
    }
}
