using System;
using System.Collections.Generic;
using System.Linq;
using LT.DigitalOffice.TimeManagementService.Database;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Repositories;
using LT.DigitalOffice.TimeManagementService.Repositories.Filters;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace LT.DigitalOffice.TimeManagementServiceUnitTests.Repositories
{
    public class CreateWorkTimeTests
    {
        private TimeManagementDbContext dbContext;
        private IWorkTimeRepository repository;

        private Guid firstProject;
        private Guid secondProject;
        private Guid firstWorker;
        private Guid secondWorker;
        private List<DbWorkTime> workTimesOfFirstWorker;
        private List<DbWorkTime> workTimesOfSecondWorker;

        private void AddAllWorkTimesToDatabase()
        {
            foreach (var workTime in workTimesOfFirstWorker)
            {
                dbContext.Add(workTime);
            }
            foreach (var workTime in workTimesOfSecondWorker)
            {
                dbContext.Add(workTime);
            }
            dbContext.SaveChanges();
        }
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<TimeManagementDbContext>()
                                    .UseInMemoryDatabase("InMemoryDatabase")
                                    .Options;
            dbContext = new TimeManagementDbContext(dbOptions);
            repository = new WorkTimeRepository(dbContext);

            firstProject = Guid.NewGuid();
            secondProject = Guid.NewGuid();
            firstWorker = Guid.NewGuid();
            secondWorker = Guid.NewGuid();

            workTimesOfFirstWorker = new List<DbWorkTime>
            {
                new DbWorkTime
                {
                    Id = Guid.NewGuid(),
                    Title = "WorkTime",
                    WorkerUserId = firstWorker,
                    ProjectId = firstProject,
                    StartTime = DateTime.Now.AddDays(-1),
                    EndTime = DateTime.Now.AddDays(-0.75)
                },
                new DbWorkTime
                {
                    Id = Guid.NewGuid(),
                    Title = "WorkTime",
                    WorkerUserId = firstWorker,
                    ProjectId = secondProject,
                    StartTime = DateTime.Now.AddDays(-0.7),
                    EndTime = DateTime.Now.AddDays(-0.45)
                }
            };

            workTimesOfSecondWorker = new List<DbWorkTime>
            {
                new DbWorkTime
                {
                    Id = Guid.NewGuid(),
                    Title = "WorkTime",
                    WorkerUserId = secondWorker,
                    ProjectId = firstProject,
                    StartTime = DateTime.Now.AddDays(-0.9),
                    EndTime = DateTime.Now.AddDays(-0.65)
                }
            };
        }

        [TearDown]
        public void CleanDb()
        {
            if (dbContext.Database.IsInMemory())
            {
                dbContext.Database.EnsureDeleted();
            }
        }

        #region CreateWorkTimeTests
        [Test]
        public void ShouldAddNewWorkTimeInDb()
        {
            Assert.That(repository.CreateWorkTime(workTimesOfFirstWorker.First()),
                Is.EqualTo(workTimesOfFirstWorker.First().Id));

            Assert.That(dbContext.WorkTimes, Is.EquivalentTo(new[] {workTimesOfFirstWorker.First()}));
        }
        #endregion

        #region GetUserWorkTimesTests
        [Test]
        public void ShouldReturnsWorkTimeWhenUsingFilterForGettingNoting()
        {
            AddAllWorkTimesToDatabase();
            var filterForGettingNothing = new WorkTimeFilter
            {
                StartTime = DateTime.Now.AddDays(-10),
                EndTime = DateTime.Now.AddDays(-5)
            };

            Assert.That(repository.GetUserWorkTimes(firstWorker, filterForGettingNothing), Is.Empty);
            Assert.That(repository.GetUserWorkTimes(secondWorker, filterForGettingNothing), Is.Empty);
        }

        [Test]
        public void ShouldReturnsWorkTimeWhenUsingFilterForGettingEverything()
        {
            AddAllWorkTimesToDatabase();
            var filterForGettingEverything = new WorkTimeFilter
            {
                StartTime = DateTime.Now.AddDays(-10),
                EndTime = DateTime.Now.AddDays(10)
            };

            Assert.That(repository.GetUserWorkTimes(firstWorker, filterForGettingEverything),
                Is.EquivalentTo(workTimesOfFirstWorker));
            Assert.That(repository.GetUserWorkTimes(secondWorker, filterForGettingEverything),
                Is.EquivalentTo(workTimesOfSecondWorker));
        }

        [Test]
        public void ShouldReturnsWorkTimeWhenUsingFilterForGettingOnlyWorkTimesOfSecondWorker()
        {
            AddAllWorkTimesToDatabase();
            var filerForGettingOnlyWorkTimeOfSecondWorker = new WorkTimeFilter
            {
                StartTime = DateTime.Now.AddDays(-0.95),
                EndTime = DateTime.Now.AddDays(-0.6)
            };

            Assert.That(repository.GetUserWorkTimes(firstWorker, filerForGettingOnlyWorkTimeOfSecondWorker), Is.Empty);
            Assert.That(repository.GetUserWorkTimes(secondWorker, filerForGettingOnlyWorkTimeOfSecondWorker),
                Is.EquivalentTo(workTimesOfSecondWorker));
        }
        #endregion
    }
}