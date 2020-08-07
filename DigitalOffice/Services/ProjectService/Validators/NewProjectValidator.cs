using FluentValidation;
using LT.DigitalOffice.ProjectService.Models;

namespace LT.DigitalOffice.ProjectService.Validators
{
    public class NewProjectValidator : AbstractValidator<NewProjectRequest>
    {
        public NewProjectValidator()
        {
            RuleFor(project => project.Name)
                    .NotEmpty()
                    .WithMessage("Project name is empty.")
                    .MaximumLength(80)
                    .WithMessage("Project name too long.")
                    .Matches("^[A-Z 0-9][A-Z a-z 0-9]+$")
                    .WithMessage("Incorrect project name.");
        }
    }
}
