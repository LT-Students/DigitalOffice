using System;
using LT.DigitalOffice.Broker.Requests;
using FluentValidation;
using LT.DigitalOffice.TimeManagementService.Commands;
using LT.DigitalOffice.TimeManagementService.Commands.Interfaces;
using LT.DigitalOffice.TimeManagementService.Database;
using MassTransit;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Mappers;
using LT.DigitalOffice.TimeManagementService.Mappers.Interfaces;
using LT.DigitalOffice.TimeManagementService.Models;
using LT.DigitalOffice.TimeManagementService.Repositories;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using LT.DigitalOffice.TimeManagementService.Validators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
            ConfigureMassTransit(services);
            services.AddMassTransitHostedService();

            ConfigureCommands(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
            ConfigureRepositories(services);
        }

        private void ConfigureMassTransit(IServiceCollection services)
        {
            services.AddMassTransit(configurator =>
            {
                configurator.UsingRabbitMq((context, factoryConfigurator) =>
                {
                    const string serviceInfoSection = "ServiceInfo";

                    var serviceName = Configuration.GetSection(serviceInfoSection)["Name"];
                    var serviceId = Configuration.GetSection(serviceInfoSection)["Id"];

                    factoryConfigurator.Host("localhost", "/", hostConfigurator =>
                    {
                        hostConfigurator.Username($"{serviceName}_{serviceId}");
                        hostConfigurator.Password(serviceId);
                    });
                });

                configurator.AddRequestClient<ICheckIfUserHasRightRequest>(
                    new Uri("rabbitmq://localhost/CheckRightsService"));
            });
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<ICreateWorkTimeCommand, CreateWorkTimeCommand>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateWorkTimeRequest>, CreateWorkTimeRequestValidator>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<CreateWorkTimeRequest, DbWorkTime>, WorkTimeMapper>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IWorkTimeRepository, WorkTimeRepository>();
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

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<TimeManagementDbContext>();

            context.Database.Migrate();
        }
    }
}
