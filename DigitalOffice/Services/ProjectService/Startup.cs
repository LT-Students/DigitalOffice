using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LT.DigitalOffice.ProjectService.Commands;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Validators;
using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using MassTransit;

namespace LT.DigitalOffice.ProjectService
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
            services.AddDbContext<ProjectServiceDbContext>(options =>
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
                        hostConfigurator.Password($"{serviceId}");
                    });
                });
            });

            ConfigCommands(services);
            ConfigRepositories(services);
            ConfigMappers(services);
            ConfigValidators(services);
        }

        private void ConfigCommands(IServiceCollection services)
        {
            services.AddTransient<IGetProjectInfoByIdCommand, GetProjectInfoByIdCommand>();
            services.AddTransient<ICreateNewProjectCommand, CreateNewProjectCommand>();
        }

        private void ConfigRepositories(IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        private void ConfigMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbProject, Project>, ProjectMapper>();
            services.AddTransient<IMapper<NewProjectRequest, DbProject>, DbProjectMapper>();
        }

        private void ConfigValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<NewProjectRequest>, NewProjectValidator>();
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
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();

            using var context = serviceScope.ServiceProvider.GetService<ProjectServiceDbContext>();

            context.Database.Migrate();
        }
    }
}