using FluentValidation;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Validator
{
    public class AddRightsForUserValidator : AbstractValidator<RightsForUserRequest>
    {
        public AddRightsForUserValidator()
        {
            RuleFor(rights => rights.UserId)
                .NotNull()
                .WithName("User Id");

            RuleFor(rights => rights.RightsId)
                .NotNull()
                .WithName("Right Id");
        }
    }
}