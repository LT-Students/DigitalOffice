using FluentValidation;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Validators
{
    public class ProjectUserValidator : AbstractValidator<AddUserToProjectRequest>
    {
        public ProjectUserValidator()
        {
            RuleFor(u => u.UserId)
                .NotEmpty()
                .NotNull();
            RuleFor(u => u.ProjectId)
                .NotEmpty()
                .NotNull();
        }
    }
}
