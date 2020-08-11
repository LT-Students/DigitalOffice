using FluentValidation;
using FluentValidation.TestHelper;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Models;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using LT.DigitalOffice.TimeManagementService.Validators;
using LT.DigitalOffice.TimeManagementService.Enums;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.TimeManagementServiceUnitTests.Validators
{
    public class CreateLeaveTimeRequestValidatorTests
    {
        private Mock<ILeaveTimeRepository> repositoryMock;
        private IValidator<CreateLeaveTimeRequest> validator;

        private CreateLeaveTimeRequest request;
        private DbLeaveTime expectedDbLeaveTime;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<ILeaveTimeRepository>();

            validator = new CreateLeaveTimeRequestValidator(repositoryMock.Object);

            request = new CreateLeaveTimeRequest
            {
                LeaveType = LeaveType.SickLeave,
                Comment = "I have a sore throat",
                StartTime = new DateTime(2020, 7, 24),
                EndTime = new DateTime(2020, 7, 27),
                WorkerUserId = Guid.NewGuid()
            };

            expectedDbLeaveTime = new DbLeaveTime
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Comment = request.Comment,
                LeaveType = request.LeaveType
            };

            repositoryMock.Setup(x => x.GetUserLeaveTimes(request.WorkerUserId))
                .Returns(new List<DbLeaveTime> { expectedDbLeaveTime });
        }

        [Test]
        public void SuccessfulWorkTimeValidation()
        {
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.TestValidate(request).ShouldNotHaveAnyValidationErrors();
        }

        #region WorkerUserId
        [Test]
        public void FailValidationEmptyWorkerUserId()
        {
            var workerUserId = Guid.Empty;
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.WorkerUserId, workerUserId);
        }
        #endregion

        #region StartTime
        [Test]
        public void FailValidationEmptyStartTime()
        {
            var startTime = new DateTime();
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.StartTime, startTime);
        }
        #endregion

        #region EndTime
        [Test]
        public void FailValidationEmptyEndTime()
        {
            var endTime = new DateTime();
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.EndTime, endTime);
        }
        #endregion

        #region Comment
        [Test]
        public void FailValidationEmptyTitle()
        {
            var comment = string.Empty;
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.Comment, comment);
        }
        #endregion

        #region ProjectId
        [Test]
        public void FailValidationEmptyProjectId()
        {
            var leaveType = new LeaveType();
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.LeaveType, leaveType);
        }
        #endregion

        #region LeaveType
        [Test]
        public void FailValidationEmptyLeaveType()
        {
            var leaveType = new LeaveType();
            repositoryMock.Setup(x => x.GetUserLeaveTimes(It.IsAny<Guid>()))
                .Returns(new List<DbLeaveTime>());

            validator.ShouldHaveValidationErrorFor(x => x.LeaveType, leaveType);
        }
        #endregion

        [Test]
        public void SuccessfulValidationOverlapWithOtherTime()
        {
            var successfulRequest = new CreateLeaveTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(-6),
                EndTime = request.StartTime.AddHours(-5.85),
                Comment = request.Comment,
                LeaveType = request.LeaveType
            };

            validator.TestValidate(successfulRequest).ShouldNotHaveAnyValidationErrors();
        }

        [Test]
        public void FailValidationIntersectionWithTheStartTime()
        {
            var failRequest = new CreateLeaveTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(-1),
                EndTime = request.EndTime.AddHours(-1),
                Comment = request.Comment,
                LeaveType = request.LeaveType
            };

            validator.TestValidate(failRequest).ShouldHaveAnyValidationError();
        }

        [Test]
        public void FailValidationIntersectionInsideTime()
        {
            var failRequest = new CreateLeaveTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(1),
                EndTime = request.EndTime.AddHours(-1),
                Comment = request.Comment,
                LeaveType = request.LeaveType
            };

            validator.TestValidate(failRequest).ShouldHaveAnyValidationError();
        }

        [Test]
        public void FailValidationIntersectionWithTheEndTime()
        {
            var failRequest = new CreateLeaveTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(1),
                EndTime = request.EndTime.AddHours(1),
                Comment = request.Comment,
                LeaveType = request.LeaveType
            };

            validator.TestValidate(failRequest).ShouldHaveAnyValidationError();
        }
    }
}