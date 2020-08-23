using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.UserService.Validators;
using NUnit.Framework;

namespace LT.DigitalOffice.UserServiceUnitTests.Validators
{
    class GetUserByEmailValidatorTests
    {
        private IValidator<string> validator;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            validator = new GetUserByEmailValidator();
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x, "");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailIsInvalid()
        {
            validator.ShouldHaveValidationErrorFor(x => x, "wrongEmail");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailTooLong()
        {
            var email = new string('a', 129) + "@gmail.com";
            validator.ShouldHaveValidationErrorFor(x => x, email);
        }

        [Test]
        public void ShouldValidateSuccessfullyWhenEmailIsValid()
        {
            var email = "example@gmail.com";
            validator.ShouldNotHaveValidationErrorFor(x => x, email);
        }
    }
}