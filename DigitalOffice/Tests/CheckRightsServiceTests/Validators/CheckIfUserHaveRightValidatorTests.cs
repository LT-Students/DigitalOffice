using System;
using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.CheckRightsService.Validators;
using NUnit.Framework;

namespace LT.DigitalOffice.CheckRightsServiceUnitTests.Validators
{
    public class CheckIfUserHaveRightValidatorTests
    {
        private IValidator<ICheckIfUserHaveRightRequest> validator;

        private class CheckIfUserHaveRightRequest : ICheckIfUserHaveRightRequest
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
            validator = new CheckIfUserHaveRightValidator();
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
    }
}