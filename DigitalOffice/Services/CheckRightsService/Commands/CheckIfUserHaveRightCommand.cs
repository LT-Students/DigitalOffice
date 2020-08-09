using FluentValidation;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;

namespace LT.DigitalOffice.CheckRightsService.Commands
{
    /// <summary>
    /// Represents command class in command pattern. Provides method for checking if user have right.
    /// </summary>
    public class CheckIfUserHaveRightCommand : ICheckIfUserHaveRightCommand
    {
        private readonly ICheckRightsRepository repository;
        private readonly IValidator<ICheckIfUserHaveRightRequest> validator;

        /// <summary>
        /// Initialize new instance of <see cref="CheckIfUserHaveRightCommand"/> class with specified repository and validator.
        /// </summary>
        /// <param name="repository">Specified repository.</param>
        /// <param name="validator">Specified validator for validating <see cref="ICheckIfUserHaveRightRequest"/></param>
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