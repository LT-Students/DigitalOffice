using FluentValidation;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
