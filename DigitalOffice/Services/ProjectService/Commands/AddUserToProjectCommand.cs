using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ProjectService.Commands.Interfaces;
using ProjectService.Database.Entities;
using ProjectService.Mappers;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using ProjectService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Commands
{
    public class AddUserToProjectCommand : IAddUserToProjectCommand
    {
        private readonly IValidator<AddUserToProjectRequest> validator;
        private readonly IProjectRepository repository;
        private readonly IMapper<AddUserToProjectRequest, DbProjectWorkerUser> workerMapper;
        private readonly IMapper<AddUserToProjectRequest, DbProjectManagerUser> managerMapper;

        public AddUserToProjectCommand(
            [FromServices] IValidator<AddUserToProjectRequest> validator,
            [FromServices] IProjectRepository repository,
            [FromServices] IMapper<AddUserToProjectRequest, DbProjectWorkerUser> workerMapper,
            [FromServices] IMapper<AddUserToProjectRequest, DbProjectManagerUser> managerMapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.workerMapper = workerMapper;
            this.managerMapper = managerMapper;
        }

        public bool Execute(AddUserToProjectRequest request)
        {
            validator.ValidateAndThrow(request);

            //check if user actually exists in UserService here

            if (request.IsManager)
            {
                return repository.AddUserToProject(
                    managerMapper.Map(request), request.ProjectId);
            }
            else
            {
                return repository.AddUserToProject(
                    workerMapper.Map(request), request.ProjectId);
            }
        }
    }
}