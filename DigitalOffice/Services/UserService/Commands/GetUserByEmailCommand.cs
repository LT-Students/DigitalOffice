using FluentValidation;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.UserService.Commands
{
    public class GetUserByEmailCommand : IGetUserByEmailCommand
    {
        private readonly IValidator<string> validator;
        private readonly IUserRepository repository;
        private readonly IMapper<DbUser, User> mapper;

        public GetUserByEmailCommand(
            [FromServices] IValidator<string> validator,
            [FromServices] IUserRepository repository,
            [FromServices] IMapper<DbUser, User> mapper)
        {
            this.validator = validator;
            this.repository = repository;
            this.mapper = mapper;
        }

        public User Execute(string userEmail)
        {
            validator.ValidateAndThrow(userEmail);

            var user = mapper.Map(repository.GetUserByEmail(userEmail));

            return user;
        }
    }
}