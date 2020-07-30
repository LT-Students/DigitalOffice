using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using UserService.Commands.Interfaces;
using UserService.Database.Entities;
using UserService.Mappers.Interfaces;
using UserService.Repositories.Interfaces;
using UserService.RestRequests;

namespace UserService.Commands
{
    public class UserCreateCommand : IUserCreateCommand
    {
        private readonly IValidator<UserCreateRequest> validator;
        private readonly IUserRepository repository;
        private readonly IMapper<UserCreateRequest, DbUser> mapper;

        public UserCreateCommand(
            [FromServices] IValidator<UserCreateRequest> validator,
            [FromServices] IUserRepository repository,
            [FromServices] IMapper<UserCreateRequest, DbUser> mapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.mapper = mapper;
        }

        public bool Execute(UserCreateRequest request)
        {
            validator.ValidateAndThrow(request);
            var user = mapper.Map(request);
            return repository.UserCreate(user);
        }
    }
}

