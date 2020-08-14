using FluentValidation;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Validators
{
    /// <summary>
    /// Validator for validating <see cref="ICheckIfUserHasRightRequest"/>.
    /// </summary>
    public class CheckIfUserHasRightValidator : AbstractValidator<ICheckIfUserHasRightRequest>
    {
        /// <summary>
        /// Initialize new instance of <see cref="CheckIfUserHasRightValidator"/> class with specified rules of validation.
        /// </summary>
        public CheckIfUserHasRightValidator()
        {
            RuleFor(request => request.RightId)
                .Must(rightId => rightId > 0)
                .WithMessage("ID of right must me positive number.");

            RuleFor(request => request.UserId)
                .NotEmpty();
        }
    }
}