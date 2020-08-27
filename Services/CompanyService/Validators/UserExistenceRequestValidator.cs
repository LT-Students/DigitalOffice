using FluentValidation;
using LT.DigitalOffice.UserService.Broker.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Validators
{
    public class UserExistenceRequestValidator : AbstractValidator<IUserExistenceRequest>
    {
        public UserExistenceRequestValidator()
        {
            RuleFor(r => r.Id)
                .NotEmpty();
        }
    }
}
