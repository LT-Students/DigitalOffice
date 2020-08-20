using FluentValidation;
using LT.DigitalOffice.CompanyService.Broker.Consumers;
using LT.DigitalOffice.CompanyService.Commands;
using LT.DigitalOffice.CompanyService.Commands.Interfaces;
using LT.DigitalOffice.CompanyService.Database;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using LT.DigitalOffice.CompanyService.Repositories;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using LT.DigitalOffice.CompanyService.Validators;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LT.DigitalOffice.CompanyService
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

            services.AddDbContext<CompanyServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddControllers();

            services.AddMassTransit(x =>
            {
                const string serviceSection = "ServiceInfo";
                string serviceId = Configuration.GetSection(serviceSection)["ID"];
                string serviceName = Configuration.GetSection(serviceSection)["Name"];

                x.AddConsumer<GetUserPositionConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("localhost", "/", hst =>
                    {
                        hst.Username($"{serviceName}_{serviceId}");
                        hst.Password(serviceId);
                    });

                    cfg.ReceiveEndpoint($"{serviceName}", e =>
                    {
                        e.ConfigureConsumer<GetUserPositionConsumer>(context);
                    });
                });
            });


            ConfigureCommands(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
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
            using var context = serviceScope.ServiceProvider.GetService<CompanyServiceDbContext>();
            context.Database.Migrate();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IPositionRepository, PositionRepository>();
            services.AddTransient<ICompanyRepository, CompanyRepository>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<DbCompany, Company>, CompanyMapper>();
            services.AddTransient<IMapper<AddCompanyRequest, DbCompany>, CompanyMapper>();
            services.AddTransient<IMapper<DbPosition, Position>, PositionMapper>();
            services.AddTransient<IMapper<AddPositionRequest, DbPosition>, PositionMapper>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<AddCompanyRequest>, AddCompanyValidator>();
            services.AddTransient<IValidator<AddPositionRequest>, AddPositionRequestValidator>();
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IAddCompanyCommand, AddCompanyCommand>();
            services.AddTransient<IAddPositionCommand, AddPositionCommand>();
            services.AddTransient<IGetPositionByIdCommand, GetPositionByIdCommand>();
            services.AddTransient<IGetCompanyByIdCommand, GetCompanyByIdCommand>();
            services.AddTransient<IAddCompanyCommand, AddCompanyCommand>();
        }
    }
}