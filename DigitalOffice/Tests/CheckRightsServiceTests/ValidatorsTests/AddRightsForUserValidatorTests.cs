using System;
using System.Collections.Generic;
using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.CheckRightsService.RestRequests;
using LT.DigitalOffice.CheckRightsService.Validator;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.ValidatorsTests
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
            validator.ShouldHaveValidationErrorFor(x => x.RightsId, null as List<int>);
        }
    }
}