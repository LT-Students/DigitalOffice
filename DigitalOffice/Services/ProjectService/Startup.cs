using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LT.DigitalOffice.ProjectService.Commands;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Validators;
using FluentValidation;
using MassTransit;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using ProjectService.Repositories.Interfaces;
using ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Broker.Requests;
using System;

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

        private void ConfigRabbitMq(IServiceCollection services)
        {
            string appSettingSection = "ServiceInfo";
            string serviceId = Configuration.GetSection(appSettingSection)["ID"];
            string serviceName = Configuration.GetSection(appSettingSection)["Name"];

            var uri = $"rabbitmq://localhost/ProjectService_{serviceName}";

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username($"{serviceName}");
                        host.Password($"{serviceName}_{serviceId}");
                    });
                });

                x.AddRequestClient<IUserExistenceRequest>(new Uri(uri));
            });
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