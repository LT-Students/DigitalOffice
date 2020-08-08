using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;

namespace LT.DigitalOffice.CheckRightsService.Commands
{
    public class CheckIfUserHaveRightCommand : ICheckIfUserHaveRightCommand
    {
        private readonly IValidator<ICheckIfUserHaveRightRequest> validator;
        private readonly ICheckRightsRepository repository;

        public CheckIfUserHaveRightCommand(ICheckRightsRepository repository,
            IValidator<ICheckIfUserHaveRightRequest> validator)
        {
            this.repository = repository;
            this.validator = validator;
        }

        public bool Execute(ICheckIfUserHaveRightRequest request)
        {
            validator.ValidateAndThrow(request);

            return repository.CheckIfUserHaveRight(request);
        }
    }
}