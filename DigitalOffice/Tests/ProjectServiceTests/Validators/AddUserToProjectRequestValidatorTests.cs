using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.ProjectService.Models;
using NUnit.Framework;
using ProjectService.Validators;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.ValidatorsTests
{
    class AddUserToProjectRequestValidatorTests
    {
        private IValidator<ProjectUser> validator;

        [SetUp]
        public void Initialization()
        {
            validator = new AddUserToProjectRequestValidator();
        }

        [Test]
        public void EmptyUserIdShouldThrowTest()
        {
            validator.ShouldHaveValidationErrorFor(x => x.UserId, new Guid());
        }

        [Test]
        public void EmptyProjectIdShouldThrowTest()
        {
            validator.ShouldHaveValidationErrorFor(x => x.ProjectId, new Guid());
        }
    }
}
