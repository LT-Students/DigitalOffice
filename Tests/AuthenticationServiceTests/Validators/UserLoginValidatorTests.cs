using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.AuthenticationService.Models;
using LT.DigitalOffice.AuthenticationService.Validators;
using NUnit.Framework;

namespace LT.DigitalOffice.AuthenticationServiceUnitTests.Validators
{
    class UserLoginValidatorTests
    {
        private IValidator<UserLoginInfoRequest> validator;

        [SetUp]
        public void SetUp()
        {
            validator = new UserLoginValidator();
        }

        [Test]
        public void EmptyLoginEmptyPassword()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "");
            validator.ShouldHaveValidationErrorFor(x => x.Password, "");
        }

        [Test]
        public void EmptyLogin()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "");
            validator.ShouldNotHaveValidationErrorFor(x => x.Password, "Example");
        }

        [Test]
        public void EmptyPassword()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, "Example@mail.com");
            validator.ShouldHaveValidationErrorFor(x => x.Password, "");
        }

        [Test]
        public void EmailIsValid()
        {
            validator.ShouldNotHaveValidationErrorFor(x => x.Email, "Example");
        }
    }
}