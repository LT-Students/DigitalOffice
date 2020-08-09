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
            try
            {
                await context.RespondAsync<IOperationResult<bool>>(new
                {
                    Body = command.Execute(context.Message),
                    IsSuccess = true,
                    Errors = new List<string>()
                });
            }
            catch (Exception exception)
            {
                await context.RespondAsync<IOperationResult<bool>>(new
                {
                    Body = false,
                    IsSuccess = false,
                    Errors = new List<string> {exception.Message}
                });
            }
        }
    }
}