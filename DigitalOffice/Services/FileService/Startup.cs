using System;
using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.FileService.Commands;
using LT.DigitalOffice.FileService.Commands.Interfaces;
using LT.DigitalOffice.FileService.Database;
using LT.DigitalOffice.FileService.Database.Entities;
using LT.DigitalOffice.FileService.Mappers;
using LT.DigitalOffice.FileService.Mappers.Interfaces;
using LT.DigitalOffice.FileService.Models;
using LT.DigitalOffice.FileService.Repositories;
using LT.DigitalOffice.FileService.Repositories.Interfaces;
using LT.DigitalOffice.FileService.Validators;
using LT.DigitalOffice.Kernel;
using LT.DigitalOffice.Kernel.Middlewares.Token;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LT.DigitalOffice.FileService
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
            services.AddDbContext<FileServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });
            services.AddControllers();

            services.Configure<TokenConfiguration>(Configuration);

            ConfigureCommands(services);
            ConfigureMappers(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureRabbitMq(services);
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IAddNewFileCommand, AddNewFileCommand>();
            services.AddTransient<IGetFileByIdCommand, GetFileByIdCommand>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbFile, File>, FileMapper>();
            services.AddTransient<IMapper<FileCreateRequest, DbFile>, FileMapper>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<FileCreateRequest>, AddNewFileValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseExceptionHandler(tempApp => tempApp.Run(CustomExceptionHandler.HandleCustomException));

            UpdateDatabase(app);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<TokenMiddleware>();

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
            using var context = serviceScope.ServiceProvider.GetService<FileServiceDbContext>();
            context.Database.Migrate();
        }

        private void ConfigureRabbitMq(IServiceCollection services)
        {
            string appSettingSection = "ServiceInfo";
            string serviceId = Configuration.GetSection(appSettingSection)["ID"];
            string serviceName = Configuration.GetSection(appSettingSection)["Name"];

            string appSettingMassTransitUris = "MassTransitUris";
            string checkTokenUri = Configuration.GetSection(appSettingMassTransitUris)["CheckToken"];

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", host =>
                    {
                        host.Username($"{serviceName}_{serviceId}");
                        host.Password(serviceId);
                    });
                });

                x.AddRequestClient<IUserJwtRequest>(new Uri(checkTokenUri));
            });

            services.AddMassTransitHostedService();
        }
    }
}