using FluentValidation;
using LT.DigitalOffice.FileService.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LT.DigitalOffice.FileService.Database.Entities;
using LT.DigitalOffice.FileService.Mappers.Interfaces;
using LT.DigitalOffice.FileService.RestRequests;
using LT.DigitalOffice.FileService.Mappers;
using LT.DigitalOffice.FileService.Repositories.Interfaces;
using LT.DigitalOffice.FileService.Commands;
using LT.DigitalOffice.FileService.Commands.Interfaces;
using LT.DigitalOffice.FileService.Validators;
using LT.DigitalOffice.FileService.Repositories;
using LT.DigitalOffice.FileService.Models;

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
            
            ConfigureCommands(services);
            ConfigureMappers(services);
            ConfigureRepositories(services);
            ConfigureValidators(services);
        }
        
        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IAddNewFileCommand, AddNewFileCommand>();
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
            services.AddTransient<IValidator<FileCreateRequest>, NewFileValidator>();
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
            using var context = serviceScope.ServiceProvider.GetService<FileServiceDbContext>();
            context.Database.Migrate();
        }
    }
}