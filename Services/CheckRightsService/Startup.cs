using LT.DigitalOffice.CheckRightsService.Broker.Consumers;
using LT.DigitalOffice.CheckRightsService.Commands;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Database;
using LT.DigitalOffice.CheckRightsService.Database.Entities;
using LT.DigitalOffice.CheckRightsService.Mappers;
using LT.DigitalOffice.CheckRightsService.Mappers.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using LT.DigitalOffice.CheckRightsService.Repositories;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LT.DigitalOffice.CheckRightsService
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
            services.AddHealthChecks();
            services.AddControllers();
            ConfigureMassTransit(services);
            services.AddMassTransitHostedService();

            services.AddDbContext<CheckRightsServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            ConfigureCommands(services);
            ConfigureMappers(services);
            ConfigureRepositories(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(tempApp => tempApp.Run(CustomExceptionHandler.HandleCustomException));
            
            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
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

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IGetRightsListCommand, GetRightsListCommand>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<ICheckRightsRepository, CheckRightsRepository>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbRight, Right>, RightsMapper>();
        }

        private void ConfigureMassTransit(IServiceCollection services)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.AddConsumer<CheckIfUserHasRightConsumer>();

                configurator.UsingRabbitMq((context, factoryConfigurator) =>
                {
                    const string serviceInfoSection = "ServiceInfo";

                    var serviceName = Configuration.GetSection(serviceInfoSection)["Name"];
                    var serviceId = Configuration.GetSection(serviceInfoSection)["Id"];

                    factoryConfigurator.Host("localhost", "/",hostConfigurator =>
                    {
                        hostConfigurator.Username($"{serviceName}_{serviceId}");
                        hostConfigurator.Password(serviceId);
                    });

                    factoryConfigurator.ReceiveEndpoint(serviceName, endpointConfigurator =>
                    {
                        endpointConfigurator.ConfigureConsumer<CheckIfUserHasRightConsumer>(context);
                    });
                });
            });
        }
    }
}