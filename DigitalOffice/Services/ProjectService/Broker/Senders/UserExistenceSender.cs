using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.ProjectService.Broker.Requests;
using LT.DigitalOffice.ProjectService.Broker.Responses;
using LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LT.DigitalOffice.ProjectService.Broker.Senders
{
    public class UserExistenceSender : ISender<Guid, IUserExistenceResponse>
    {
        private readonly IRequestClient<IUserExistenceRequest> requestClient;

        public UserExistenceSender([FromServices] IRequestClient<IUserExistenceRequest> requestClient)
        {
            this.requestClient = requestClient;
        }

        public async Task<Response<IOperationResult<IUserExistenceResponse>>>
            GetResponseFromBroker(Guid id)
        {
            var response = await requestClient.GetResponse<IOperationResult<IUserExistenceResponse>>(new
            {
                Id = id
            });

            if (!response.Message.IsSuccess)
            {
                throw new Exception();
            }

            if (response.Message.Errors.Count != 0)
            {
                throw new Exception(); // TODO: Add processing logic of exceptions
            }

            return response;
        }
    }
}
