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
                .WithMessage("User id is empty.");
            
            RuleFor(rights => rights.RightsId)
                .NotEmpty()
                .WithMessage("No rights to add.");
        }
    }
}