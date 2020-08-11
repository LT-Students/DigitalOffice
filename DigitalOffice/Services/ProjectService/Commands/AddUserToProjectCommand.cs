using FluentValidation;
using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ProjectService.Commands.Interfaces;

namespace ProjectService.Commands
{
    public class AddUserToProjectCommand : IAddUserToProjectCommand
    {
        private readonly IValidator<AddUserToProjectRequest> validator;
        private readonly IProjectRepository repository;
        private readonly IMapper<AddUserToProjectRequest, DbProjectWorkerUser> projectUserMapper;

        public AddUserToProjectCommand(
            [FromServices] IValidator<AddUserToProjectRequest> validator,
            [FromServices] IProjectRepository repository,
            [FromServices] IMapper<AddUserToProjectRequest, DbProjectWorkerUser> projectUserMapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.projectUserMapper = projectUserMapper;
        }

        public bool Execute(AddUserToProjectRequest request)
        {
            validator.ValidateAndThrow(request);

            // TODO: check if user actually exists in UserService here

            return repository.AddUserToProject(
                projectUserMapper.Map(request), request.ProjectId);
        }
    }
}