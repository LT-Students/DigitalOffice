using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using LT.DigitalOffice.CheckRightsService.Validator;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Validators
{
    public class AddRightsForUserValidatorTests
    {
        private IValidator<RightsForUserRequest> validator;

        [SetUp]
        public void Initialize()
        {
            validator = new AddRightsForUserValidator();
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenUserIdNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.UserId, Guid.Empty);
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenRightsIdIsNull()
        {
            validator.ShouldHaveValidationErrorFor(x => x.RightsIds, null as List<int>);
        }
    }
}