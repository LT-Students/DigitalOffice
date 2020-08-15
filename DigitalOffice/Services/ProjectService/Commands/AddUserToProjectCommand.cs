using FluentValidation;
using LT.DigitalOffice.ProjectService.Broker.Responses;
using LT.DigitalOffice.ProjectService.Broker.Senders.Interfaces;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace ProjectService.Commands
{
    public class AddUserToProjectCommand : IAddUserToProjectCommand
    {
        private readonly IValidator<AddUserToProjectRequest> validator;
        private readonly IProjectRepository repository;
        private readonly IMapper<AddUserToProjectRequest, DbProjectWorkerUser> projectUserMapper;
        private readonly ISender<Guid, IUserExistenceResponse> userExistenceSender;

        public AddUserToProjectCommand(
            [FromServices] IValidator<AddUserToProjectRequest> validator,
            [FromServices] IProjectRepository repository,
            [FromServices] IMapper<AddUserToProjectRequest, DbProjectWorkerUser> projectUserMapper,
            [FromServices] ISender<Guid, IUserExistenceResponse> userExistenceSender)
        {
            this.validator = validator;
            this.repository = repository;
            this.projectUserMapper = projectUserMapper;
            this.userExistenceSender = userExistenceSender;
        }

        public async Task<bool> Execute(AddUserToProjectRequest request)
        {
            validator.ValidateAndThrow(request);

            var brokerResponse = await userExistenceSender.GetResponseFromBroker(request.UserId);

            if (!brokerResponse.Message.Body.Exists)
            {
                throw new ArgumentException("User does not exist.");
            }

            return repository.AddUserToProject(projectUserMapper.Map(request), request.ProjectId);
        }
    }
}