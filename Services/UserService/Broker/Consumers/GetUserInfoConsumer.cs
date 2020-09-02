using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Broker.Responses;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.UserService.Broker.Consumers
{
    /// <summary>
    /// Consumer for getting information about the user.
    /// </summary>
    public class GetUserInfoConsumer : IConsumer<IGetUserInfoRequest>
    {
        private readonly IRequestClient<IGetUserPositionRequest> client;
        private readonly IMapper<DbUser, IUserPositionResponse, object> mapper;
        private readonly IUserRepository repository;

        public GetUserInfoConsumer(
            [FromServices] IUserRepository repository,
            [FromServices] IRequestClient<IGetUserPositionRequest> client,
            [FromServices] IMapper<DbUser, IUserPositionResponse, object> mapper)
        {
            this.repository = repository;
            this.client = client;
            this.mapper = mapper;
        }

        public async Task Consume(ConsumeContext<IGetUserInfoRequest> context)
        {
            var response = OperationResultWrapper.CreateResponse(GetUserInfo, context.Message.UserId);

            await context.RespondAsync<IOperationResult<IUserInfoResponse>>(response);
        }

        private object GetUserInfo(Guid userId)
        {
            var response = client.GetResponse<IOperationResult<IUserPositionResponse>>(
                new
                {
                    UserId = userId
                }).Result;

            if (!response.Message.IsSuccess)
            {
                throw new Exception(string.Join(", ", response.Message.Errors));
            }

            var dbUser = repository.GetUserInfoById(userId);

            return mapper.Map(dbUser, response.Message.Body);
        }
    }
}