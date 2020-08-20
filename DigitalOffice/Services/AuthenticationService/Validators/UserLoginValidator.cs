using FluentValidation;
using LT.DigitalOffice.AuthenticationService.Models;

namespace LT.DigitalOffice.AuthenticationService.Validators
{
    public class UserLoginValidator : AbstractValidator<UserLoginInfoRequest>
    {
        public UserLoginValidator()
        {
            RuleFor(user => user.Email)
                .NotEmpty();

            RuleFor(user => user.Password)
                .NotEmpty();
        }
    }
}