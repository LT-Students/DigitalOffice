using FluentValidation;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.CheckRightsService.Commands
{
    public class AddRightsForUserCommand : IAddRightsForUserCommand
    {
        private readonly ICheckRightsRepository repository;
        private readonly IValidator<RightsForUserRequest> validator;

        public AddRightsForUserCommand(
            [FromServices] ICheckRightsRepository repository,
            [FromServices] IValidator<RightsForUserRequest> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public bool Execute(RightsForUserRequest request)
        {
            validator.ValidateAndThrow(request);

            return repository.AddRightsToUser(request);
        }
    }
}