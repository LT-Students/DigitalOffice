using System;
using FluentValidation;
using GreenPipes;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Broker.Responses;
using LT.DigitalOffice.Kernel;
using LT.DigitalOffice.Kernel.Middlewares.Token;
using LT.DigitalOffice.UserService.Broker.Consumers;
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

namespace LT.DigitalOffice.UserService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks();

            services.AddControllers();

            services.AddDbContext<UserServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.Configure<TokenConfiguration>(Configuration);

            ConfigureCommands(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
            ConfigureRabbitMq(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(tempApp => tempApp.Run(CustomExceptionHandler.HandleCustomException));

            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<TokenMiddleware>();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }

        private void UpdateDatabase(IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope();
            using var context = serviceScope.ServiceProvider.GetService<UserServiceDbContext>();
            context.Database.Migrate();
        }

        private void ConfigureRabbitMq(IServiceCollection services)
        {
            var appSettingSection = "ServiceInfo";
            var serviceId = Configuration.GetSection(appSettingSection)["ID"];
            var serviceName = Configuration.GetSection(appSettingSection)["Name"];

            var appSettingMassTransitUris = "MassTransitUris";
            var checkTokenUri = Configuration.GetSection(appSettingMassTransitUris)["CheckToken"];
            var companyServiceUri = Configuration.GetSection(appSettingMassTransitUris)["CompanyService"];

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserLoginConsumer>();
                x.AddConsumer<GetUserInfoConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username($"{serviceName}_{serviceId}");
                        host.Password(serviceId);
                    });

                    cfg.ReceiveEndpoint($"{serviceName}_AuthenticationService", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));

                        ep.ConfigureConsumer<UserLoginConsumer>(context);
                    });

                    cfg.ReceiveEndpoint($"{serviceName}",
                        ep => { ep.ConfigureConsumer<GetUserInfoConsumer>(context); });
                });

                x.AddRequestClient<IUserJwtRequest>(new Uri(checkTokenUri));
                x.AddRequestClient<IGetUserPositionRequest>(new Uri(companyServiceUri));
            });

            services.AddMassTransitHostedService();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbUser, IUserPositionResponse, object>,
                UserMapper>();
            services.AddTransient<IMapper<UserCreateRequest, DbUser>, UserMapper>();
            services.AddTransient<IMapper<DbUser, User>, UserMapper>();
            services.AddTransient<IMapper<EditUserRequest, DbUser>, UserMapper>();
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IUserCreateCommand, UserCreateCommand>();
            services.AddTransient<IEditUserCommand, EditUserCommand>();
            services.AddTransient<IGetUserByEmailCommand, GetUserByEmailCommand>();
            services.AddTransient<IGetUserByIdCommand, GetUserByIdCommand>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<EditUserRequest>, EditUserRequestValidator>();
            services.AddTransient<IValidator<UserCreateRequest>, UserCreateRequestValidator>();
            services.AddTransient<IValidator<string>, GetUserByEmailValidator>();
        }
    }
}