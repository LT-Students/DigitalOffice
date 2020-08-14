using System;
using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Validators;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Validators
{
    public class CheckIfUserHasRightValidatorTests
    {
        private IValidator<ICheckIfUserHasRightRequest> validator;

        private class CheckIfUserHaveRightRequest : ICheckIfUserHasRightRequest
        {
            public CheckIfUserHaveRightRequest(int rightId, Guid userId)
            {
                RightId = rightId;
                UserId = userId;
            }

            public int RightId { get; }
            public Guid UserId { get; }
        }
        
        [SetUp]
        public void SetUp()
        {
            validator = new CheckIfUserHasRightValidator();
        }
        
        [Test]
        public void ShouldHaveValidationErrorIfRightIdIsNotPositive()
        {
            validator.ShouldHaveValidationErrorFor(request => request.RightId,
                new CheckIfUserHaveRightRequest(-1, Guid.Empty));
        }

        [Test]
        public void ShouldHaveValidationErrorIfUserIdIsEmpty()
        {
            validator.ShouldHaveValidationErrorFor(request => request.UserId,
                new CheckIfUserHaveRightRequest(1, Guid.Empty));
        }

        [Test]
        public void ShouldNotHaveAnyValidationErrorsWhenRequestIsValid()
        {
            validator.TestValidate(new UnitTestLibrary.CheckIfUserHaveRightRequest(1, Guid.NewGuid()))
                .ShouldNotHaveAnyValidationErrors();
        }
    }
}