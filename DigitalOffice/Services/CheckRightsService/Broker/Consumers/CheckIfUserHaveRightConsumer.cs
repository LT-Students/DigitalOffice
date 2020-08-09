using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.CheckRightsService.Broker.Consumers
{
    public class CheckIfUserHaveRightConsumer : IConsumer<ICheckIfUserHaveRightRequest>
    {
        private readonly ICheckIfUserHaveRightCommand command;

        public CheckIfUserHaveRightConsumer([FromServices]ICheckIfUserHaveRightCommand command)
        {
            this.command = command;
        }

        public async Task Consume(ConsumeContext<ICheckIfUserHaveRightRequest> context)
        {
            var body = false;
            var isSuccess = true;
            var exceptions = new List<string>();

            try
            {
                body = command.Execute(context.Message);
            }
            catch (Exception e)
            {
                exceptions.Add(e.Message);
                isSuccess = false;
            }

            await context.RespondAsync<IOperationResult<bool>>(new
            {
                IsSuccess = isSuccess,
                Body = body,
                Exceptions = exceptions
            });
        }
    }
}