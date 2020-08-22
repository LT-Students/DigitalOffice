using FluentValidation;
using LT.DigitalOffice.AuthenticationService.Commands;
using LT.DigitalOffice.AuthenticationService.Commands.Interfaces;
using LT.DigitalOffice.AuthenticationService.Models;
using LT.DigitalOffice.AuthenticationService.Token;
using LT.DigitalOffice.AuthenticationService.Token.Interfaces;
using LT.DigitalOffice.AuthenticationService.Validators;
using LT.DigitalOffice.Broker.Requests;
using MassTransit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace LT.DigitalOffice.AuthenticationService
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
            ConfigJwt(services);

            services.AddHealthChecks();

            services.AddControllers();

            ConfigureRabbitMq(services);

            services.AddMassTransitHostedService();

            ConfigureCommands(services);
            ConfigureValidators(services);
        }

        private void ConfigJwt(IServiceCollection services)
        {
            var signingKey = new SigningSymmetricKey();
            var signingDecodingKey = (IJwtSigningDecodingKey)signingKey;

            services.AddSingleton<IJwtSigningEncodingKey>(signingKey);
            services.AddTransient<INewToken, NewToken>();

            services.Configure<TokenOptions>(Configuration.GetSection("TokenSettings"));

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = Configuration.GetSection("TokenSettings:TokenIssuer").Value,
                    ValidateAudience = true,
                    ValidAudience = Configuration.GetSection("TokenSettings:TokenAudience").Value,
                    ValidateLifetime = true,
                    IssuerSigningKey = signingDecodingKey.GetKey(),
                    ValidateIssuerSigningKey = true
                };
            });
        }

        private void ConfigureRabbitMq(IServiceCollection services)
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
                        host.Username($"{serviceName}_{serviceId}");
                        host.Password(serviceId);
                    });
                });

                x.AddRequestClient<IUserCredentialsRequest>(new Uri(uri));
            });
        }

        private void ConfigureCommands(IServiceCollection services)
        {
            services.AddTransient<IUserLoginCommand, UserLoginCommand>();
        }

        private void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<UserLoginInfoRequest>, UserLoginValidator>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}