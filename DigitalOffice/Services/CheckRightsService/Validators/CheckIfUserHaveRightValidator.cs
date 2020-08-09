using FluentValidation;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Validators
{
    /// <summary>
    /// Validator for validating <see cref="ICheckIfUserHaveRightRequest"/>.
    /// </summary>
    public class CheckIfUserHaveRightValidator : AbstractValidator<ICheckIfUserHaveRightRequest>
    {
        /// <summary>
        /// Initialize new instance of <see cref="CheckIfUserHaveRightValidator"/> class with specified rules of validation.
        /// </summary>
        public CheckIfUserHaveRightValidator()
        {
            RuleFor(request => request.RightId)
                .Must(rightId => rightId > 0)
                .WithMessage("ID of right must me positive number.");

            RuleFor(request => request.UserId)
                .NotEmpty();
        }
    }
}