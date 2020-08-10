using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Validators;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.ProjectServiceUnitTests.Validators
{
    public class CreateNewProjectRequestValidatorTests
    {
        private IValidator<NewProjectRequest> validator;

        [SetUp]
        public void SetUp()
        {
            validator = new NewProjectValidator();
        }

        [Test]
        public void ValidationFailEmptyNameProjectTest()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Name, "");

            validator.ShouldNotHaveValidationErrorFor(x => x.DepartmentId, Guid.NewGuid());
            validator.ShouldNotHaveValidationErrorFor(x => x.Description, "New project for Lanit-Tercom");
            validator.ShouldNotHaveValidationErrorFor(x => x.IsActive, true);
        }

        [Test]
        public void ValidationSuccesfullyNumberInNameProjectTest()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Name, "12DigitalOffice24322525");

            validator.ShouldNotHaveValidationErrorFor(x => x.DepartmentId, Guid.NewGuid());
            validator.ShouldNotHaveValidationErrorFor(x => x.Description, "New project for Lanit-Tercom");
            validator.ShouldNotHaveValidationErrorFor(x => x.IsActive, true);
        }
    }
}
