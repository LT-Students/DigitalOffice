using InternalModels;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using LT.DigitalOffice.UserService.RestRequests;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Repositories.Interfaces;

namespace LT.DigitalOffice.UserService.Broker.Consumers
{
    public class UserExistanceConsumer : IConsumer<UserExistenceRequest>
    {
        IUserRepository repository;

        public UserExistanceConsumer([FromServices] IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserExistanceRequest> context)
        {
            var response = new UserExistenceResponse
            {
                Exists = repository.ContainsUserWithId(context.Message.Id)
            };

            await context.RespondAsync(response);
        }
    }
}
