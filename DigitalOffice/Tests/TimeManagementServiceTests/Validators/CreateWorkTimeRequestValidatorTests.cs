using FluentValidation;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Models;
using LT.DigitalOffice.TimeManagementService.Repositories.Filters;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using LT.DigitalOffice.TimeManagementService.Validators;

namespace LT.DigitalOffice.TimeManagementServiceUnitTests.Validators
{
    public class CreateWorkTimeRequestValidatorTests
    {
        private Mock<IWorkTimeRepository> repositoryMock;

        private IValidator<CreateWorkTimeRequest> validator;

        private CreateWorkTimeRequest request;

        [SetUp]
        public void Setup()
        {
            repositoryMock = new Mock<IWorkTimeRepository>();

            repositoryMock.Setup(x => x.GetUserWorkTimes(It.IsAny<Guid>(), It.IsAny<WorkTimeFilter>()))
                .Returns(new List<DbWorkTime>());

            validator = new CreateWorkTimeRequestValidator(repositoryMock.Object);

            request = new CreateWorkTimeRequest
            {
                WorkerUserId = Guid.NewGuid(),
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(6),
                Title = "Worked...",
                ProjectId = Guid.NewGuid(),
                Description = "Did something"
            };
        }

        [Test]
        public void SuccessfulWorkTimeValidation()
        {
            validator.TestValidate(request).ShouldNotHaveAnyValidationErrors();
        }

        #region WorkerUserId
        [Test]
        public void FailValidationEmptyWorkerUserId()
        {
            request.WorkerUserId = Guid.Empty;

            var result = validator.Validate(request);
            Assert.IsFalse(result.IsValid);
        }
        #endregion

        #region StartTime
        [Test]
        public void FailValidationStartTimeTooEarly()
        {
            var startTime = DateTime.Now.AddDays(CreateWorkTimeRequestValidator.ToDay).AddHours(1);
            validator.ShouldHaveValidationErrorFor(x => x.StartTime, startTime);
        }

        [Test]
        public void FailValidationStartTimeTooLate()
        {
            var startTime = DateTime.Now.AddDays(CreateWorkTimeRequestValidator.FromDay).AddHours(-1);
            validator.ShouldHaveValidationErrorFor(x => x.StartTime, startTime);
        }
        #endregion


        #region Title
        [Test]
        public void FailValidationEmptyTitle()
        {
            request.Title = string.Empty;

            validator.TestValidate(request).ShouldHaveAnyValidationError();
        }
        #endregion

        #region ProjectId
        [Test]
        public void FailValidationEmptyProjectId()
        {
            request.ProjectId = Guid.Empty;

            validator.TestValidate(request).ShouldHaveAnyValidationError();
        }
        #endregion

        [Test]
        public void FailStartTimeBeforeEndTime()
        {
            var temp = request.StartTime;
            request.StartTime = request.EndTime;
            request.EndTime = temp;

            validator.TestValidate(request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void FailDueToWoringLimit()
        {
            var tooManyMinutes = CreateWorkTimeRequestValidator.WorkingLimit.TotalMinutes + 1;
            request.EndTime = request.StartTime.AddMinutes(tooManyMinutes);

            validator.TestValidate(request).ShouldHaveAnyValidationError();
        }

        [Test]
        public void FailValidationOverlapWithOther()
        {
            var dbWorkTimes = new List<DbWorkTime>
            {
                new DbWorkTime
                {
                    WorkerUserId = request.WorkerUserId,
                    StartTime = request.StartTime,
                    EndTime = request.EndTime,
                    Title = request.Title,
                    ProjectId = request.ProjectId,
                    Description = request.Description
                }
            };

            repositoryMock.Setup(x => x.GetUserWorkTimes(request.WorkerUserId, It.IsAny<WorkTimeFilter>()))
                .Returns(dbWorkTimes);

            var successfulRequest = new CreateWorkTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(-6),
                EndTime = request.StartTime.AddHours(-5.85),
                Title = request.Title,
                ProjectId = request.ProjectId,
                Description = request.Description
            };

            validator.TestValidate(successfulRequest).ShouldNotHaveAnyValidationErrors();

            var failRequestIntersectionWithTheBeginning = new CreateWorkTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(-1),
                EndTime = request.EndTime.AddHours(-1),
                Title = request.Title,
                ProjectId = request.ProjectId,
                Description = request.Description
            };

            validator.TestValidate(failRequestIntersectionWithTheBeginning).ShouldHaveAnyValidationError();

            var failRequestIntersectionInside = new CreateWorkTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(1),
                EndTime = request.EndTime.AddHours(-1),
                Title = request.Title,
                ProjectId = request.ProjectId,
                Description = request.Description
            };

            validator.TestValidate(failRequestIntersectionInside).ShouldHaveAnyValidationError();

            var failRequestIntersectionWithTheEnding = new CreateWorkTimeRequest
            {
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime.AddHours(1),
                EndTime = request.EndTime.AddHours(1),
                Title = request.Title,
                ProjectId = request.ProjectId,
                Description = request.Description
            };

            validator.TestValidate(failRequestIntersectionWithTheEnding).ShouldHaveAnyValidationError();
        }
    }
}