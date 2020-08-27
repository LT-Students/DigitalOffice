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
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectService.Commands
{
    public class AddUsersToProjectCommand : IAddUsersToProjectCommand
    {
        private readonly IValidator<ProjectUser> validator;
        private readonly IProjectRepository repository;
        private readonly IMapper<ProjectUser, DbProjectWorkerUser> projectUserMapper;
        private readonly ISender<Guid, IUserExistenceResponse> userExistenceSender;

        public AddUsersToProjectCommand(
            [FromServices] IValidator<ProjectUser> validator,
            [FromServices] IProjectRepository repository,
            [FromServices] IMapper<ProjectUser, DbProjectWorkerUser> projectUserMapper,
            [FromServices] ISender<Guid, IUserExistenceResponse> userExistenceSender)
        {
            this.validator = validator;
            this.repository = repository;
            this.projectUserMapper = projectUserMapper;
            this.userExistenceSender = userExistenceSender;
        }

        public async Task<IEnumerable<bool>> Execute(AddUserToProjectRequest request)
        {
            var result = new List<bool>();

            foreach (var user in request.UsersToAdd)
            {
                validator.ValidateAndThrow(user);

                var brokerResponse = await userExistenceSender.GetResponseFromBroker(user.UserId);

                if (!brokerResponse.Message.Body.Exists)
                {
                    throw new ArgumentException("User does not exist.");
                }

                result.Add(repository.AddUserToProject(projectUserMapper.Map(user), user.ProjectId));
            }

            return result;
        }
    }
}