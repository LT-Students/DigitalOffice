using InternalModels;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Repositories.Interfaces;

namespace UserService.Broker.Consumers
{
    public class UserExistanceConsumer : IConsumer<UserExistanceRequest>
    {
        IUserRepository repository;

        public UserExistanceConsumer([FromServices] IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<UserExistanceRequest> context)
        {
            var response = new UserExistanceResponse
            {
                Exists = repository.ContainsUserWithId(context.Message.Id)
            };

            await context.RespondAsync(response);
        }
    }
}
