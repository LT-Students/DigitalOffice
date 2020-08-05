using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UserService.Broker.Consumers;
using UserService.Database;
using UserService.Repositories;
using UserService.Repositories.Interfaces;

namespace UserService
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

            services.AddDbContext<UserServiceDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLConnectionString"));
            });

            services.AddControllers();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<UserExistanceConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    const string serviceSection = "ServiceInfo";
                    string serviceId = Configuration.GetSection(serviceSection)["ID"];
                    string serviceName = Configuration.GetSection(serviceSection)["Name"];

                    cfg.Host("localhost", "/", h =>
                    {
                        h.Username($"{serviceName}_{serviceId}");
                        h.Password($"{serviceId}");
                    });

                    cfg.ReceiveEndpoint($"{serviceName}", ep =>
                    {
                        ep.ConfigureConsumer<UserExistanceConsumer>(context);
                    });
                }
            })
                // wip

            ConfigureRepositories(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
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
