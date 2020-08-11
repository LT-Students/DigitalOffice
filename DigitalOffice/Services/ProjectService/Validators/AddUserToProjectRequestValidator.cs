using FluentValidation;
using LT.DigitalOffice.ProjectService.Models;

namespace ProjectService.Validators
{
    public class AddUserToProjectRequestValidator : AbstractValidator<AddUserToProjectRequest>
    {
        public AddUserToProjectRequestValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.ProjectId)
                .NotEmpty()
                .NotNull();
            RuleFor(r => r.IsManager)
                .NotNull();
        }
    }
}
