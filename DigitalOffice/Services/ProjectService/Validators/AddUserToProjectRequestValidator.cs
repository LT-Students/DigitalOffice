using FluentValidation;
using LT.DigitalOffice.ProjectService.Models;

namespace ProjectService.Validators
{
    public class AddUserToProjectRequestValidator : AbstractValidator<ProjectUser>
    {
        public AddUserToProjectRequestValidator()
        {
            RuleFor(r => r.UserId)
                .NotEmpty();

            RuleFor(r => r.ProjectId)
                .NotEmpty();

            RuleFor(r => r.IsManager)
                .NotNull();
        }
    }
}
