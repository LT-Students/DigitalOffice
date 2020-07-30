using FluentValidation;
using FluentValidation.TestHelper;
using NUnit.Framework;
using UserService.RestRequests;
using UserService.Validators;

namespace UserServiceUnitTests.Validators
{
    public class UserCreateRequestValidatorTests
    {
        private IValidator<UserCreateRequest> validator;

        [SetUp] 
        public void Initialize()
        {
            validator = new UserCreateRequestValidator();
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenFirstNameIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, "");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenFirstNameContainsNumbers()
        {
            validator.ShouldHaveValidationErrorFor(x => x.FirstName, "Example1");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenLastNameFirstLetterIsNotUpperCase()
        {
            validator.ShouldHaveValidationErrorFor(x => x.LastName, "example");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenLastNameRestLettersAreNotLowerCase()
        {
            validator.ShouldHaveValidationErrorFor(x => x.LastName, "EXAMPLE");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenMiddleNameTooLong()
        {
            var middleName = "Example" + new string('a', 30);
            validator.ShouldHaveValidationErrorFor(x => x.MiddleName, middleName);
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenMiddleNameConsistOnlyOneLetter()
        {
            validator.ShouldHaveValidationErrorFor(x => x.MiddleName, "E");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailIsInvalid()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Email, "wrongEmail");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenEmailTooLong()
        {
            var email = new string('a', 50) + "@gmail.com";
            validator.ShouldHaveValidationErrorFor(x => x.Email, email);
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenStatusTooLong()
        {
            var status = new string('a', 300) + "@gmail.com";
            validator.ShouldHaveValidationErrorFor(x => x.Status, status);
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenPasswordIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(x => x.Password, "");
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenAllFieldsAreEmpty()
        {
            var user = new UserCreateRequest();
            Assert.Throws<ValidationException>(() => validator.ValidateAndThrow(user));
        }

        [Test]
        public void ShouldThrowValidationExceptionWhenDataIsValid()
        {
            var request = new UserCreateRequest
            {
                FirstName = "Example",
                LastName = "Example",
                MiddleName = "Example",
                Email = "Example@gmail.com",
                Status = "Example",
                Password = "Example"
            };
            Assert.DoesNotThrow(() => validator.ValidateAndThrow(request));
        }
    }
}
