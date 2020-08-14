using FluentValidation;
using LT.DigitalOffice.UserService.Broker.Consumers;
using LT.DigitalOffice.UserService.Broker.Requests;
using LT.DigitalOffice.UserService.Commands;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.Repositories;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using LT.DigitalOffice.UserService.Validators;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace LT.DigitalOffice.UserService
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
            services.AddDbContext<UserServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddHealthChecks();
            services.AddControllers();
            services.AddMassTransitHostedService();

            ConfigRabbitMQ(services);
            ConfigBrokerConsumers(services);

            ConfigureCommands(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
        }

        private void ConfigRabbitMQ(IServiceCollection services)
        {
            string appSettingSection = "ServiceInfo";
            string serviceId = Configuration.GetSection(appSettingSection)["ID"];
            string serviceName = Configuration.GetSection(appSettingSection)["Name"];

            var uri = $"rabbitmq://localhost/UserService_{serviceName}";

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
            });
        }

        private void ConfigBrokerConsumers(IServiceCollection services)
        {
            services.AddTransient<IConsumer<UserExistenceRequest>, UserExistenceConsumer>();
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IUserCreateCommand, UserCreateCommand>();
            services.AddTransient<IGetUserByIdCommand, GetUserByIdCommand>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<UserCreateRequest, DbUser>, UserMapper>();
            services.AddTransient<IMapper<DbUser, User>, UserMapper>();
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
            using var context = serviceScope.ServiceProvider.GetService<UserServiceDbContext>();
            context.Database.Migrate();
        }
    }
}