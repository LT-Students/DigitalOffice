using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.UserService.Broker.Consumers.Requests;
using LT.DigitalOffice.UserService.Broker.Consumers.Responses;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Broker.Consumers
{
    public class UserExistenceConsumer : IConsumer<IUserExistenceRequest>
    {
        private readonly IUserRepository repository;

        public UserExistenceConsumer([FromServices] IUserRepository repository)
        {
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<IUserExistenceRequest> context)
        {
            var response = new 
            {
                IsSuccess = true,
                Errors = new List<string>(),
                Body = new { Exists = repository.UserExists(context.Message.Id) }
            };

            await context.RespondAsync<IOperationResult<IUserExistenceResponse>>(response);
        }
    }
}
