using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectService.Commands;
using ProjectService.Commands.Interfaces;
using ProjectService.Database;
using ProjectService.Database.Entities;
using ProjectService.Mappers;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using ProjectService.Repositories;
using ProjectService.Repositories.Interfaces;
using ProjectService.Validators;

namespace ProjectService
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

            services.AddDbContext<ProjectServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddControllers();

            ConfigureCommands(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
            ConfigureMappers(services);
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IAddUserToProjectCommand, AddUserToProjectCommand>();
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IProjectRepository, ProjectRepository>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<AddUserToProjectRequest>, AddUserToProjectRequestValidator>();
        }

        private void ConfigureMappers(IServiceCollection services)
        {
            services.AddTransient<IMapper<AddUserToProjectRequest, DbProjectWorkerUser>, WorkerMapper>();
            services.AddTransient<IMapper<AddUserToProjectRequest, DbProjectManagerUser>, ManagerMapper>();
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