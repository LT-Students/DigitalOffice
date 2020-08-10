﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;

namespace LT.DigitalOffice.CheckRightsService.Broker.Consumers
{
    public class CheckIfUserHaveRightConsumer : IConsumer<ICheckIfUserHaveRightRequest>
    {
        private readonly IValidator<ICheckIfUserHaveRightRequest> validator;
        private readonly ICheckRightsRepository repository;

        public CheckIfUserHaveRightConsumer(IValidator<ICheckIfUserHaveRightRequest> validator, ICheckRightsRepository repository)
        {
            this.validator = validator;
            this.repository = repository;
        }

        public async Task Consume(ConsumeContext<ICheckIfUserHaveRightRequest> context)
        {
            try
            {
                validator.ValidateAndThrow(context.Message);
                await context.RespondAsync<IOperationResult<bool>>(new
                {
                    Body = repository.CheckIfUserHaveRight(context.Message),
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