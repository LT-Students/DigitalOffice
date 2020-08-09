using FluentValidation;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Validator
{
    public class AddRightsForUserValidator : AbstractValidator<RightsForUserRequest>
    {
        public AddRightsForUserValidator()
        {
            RuleFor(rights => rights.UserId)
                .NotEmpty()
                .WithName("User Id");

            RuleFor(rights => rights.RightsId)
                .NotEmpty()
                .WithName("Right Id");
        }
    }
}