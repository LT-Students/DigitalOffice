using FluentValidation;
using LT.DigitalOffice.ProjectService.Broker.Requests;
using LT.DigitalOffice.ProjectService.Broker.Responses;
using LT.DigitalOffice.ProjectService.Broker.Senders;
using LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces;
using LT.DigitalOffice.ProjectService.Commands;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using LT.DigitalOffice.ProjectService.Validators;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectService.Commands;
using ProjectService.Validators;
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

            services.AddHealthChecks();
            services.AddControllers();
            services.AddMassTransitHostedService();

            ConfigRabbitMQ(services);
            ConfigBrokerSenders(services);

            ConfigCommands(services);
            ConfigRepositories(services);
            ConfigMappers(services);
            ConfigValidators(services);
        }

        private void ConfigRabbitMQ(IServiceCollection services)
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

        private void ConfigBrokerSenders(IServiceCollection services)
        {
            services.AddTransient<ISender<Guid, IUserExistenceResponse>, UserExistenceSender>();
        }

        private void ConfigCommands(IServiceCollection services)
        {
            services.AddTransient<IGetProjectInfoByIdCommand, GetProjectInfoByIdCommand>();
            services.AddTransient<ICreateNewProjectCommand, CreateNewProjectCommand>();
            services.AddTransient<IAddUserToProjectCommand, AddUserToProjectCommand>();
        }

        private void ConfigRepositories(IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        private void ConfigMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbProject, Project>, ProjectMapper>();
            services.AddTransient<IMapper<NewProjectRequest, DbProject>, ProjectMapper>();
            services.AddTransient<IMapper<AddUserToProjectRequest, DbProjectWorkerUser>, ProjectUserMapper>();
        }

        private void ConfigValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<NewProjectRequest>, NewProjectValidator>();
            services.AddTransient<IValidator<AddUserToProjectRequest>, AddUserToProjectRequestValidator>();
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