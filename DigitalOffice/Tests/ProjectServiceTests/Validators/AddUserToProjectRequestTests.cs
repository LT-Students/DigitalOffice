using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using ProjectService.Models;
using ProjectService.Validators;
using System;

namespace ProjectServiceUnitTests.ValidatorsTests
{
    class AddUserToProjectRequestTests
    {
        private IValidator<AddUserToProjectRequest> validator;

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
