using FluentValidation;
using LT.DigitalOffice.Broker.Requests;

namespace LT.DigitalOffice.CheckRightsService.Validators
{
    public class CheckIfUserHaveRightValidator : AbstractValidator<ICheckIfUserHaveRightRequest>
    {
        public CheckIfUserHaveRightValidator()
        {
            RuleFor(request => request)
                .NotNull();
            
            RuleFor(request => request.RightId)
                .Must(rightId => rightId > 0)
                .WithMessage("Id of right must me positive number.");

            RuleFor(request => request.UserId)
                .NotEmpty();
        }
    }
}