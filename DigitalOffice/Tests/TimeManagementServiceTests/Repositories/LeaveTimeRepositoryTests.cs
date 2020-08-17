using LT.DigitalOffice.TimeManagementService.Database;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Repositories;
using LT.DigitalOffice.TimeManagementService.Repositories.Interfaces;
using LT.DigitalOffice.TimeManagementService.Enums;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;

namespace LT.DigitalOffice.TimeManagementServiceUnitTests.Repositories
{
    public class CreateLeaveTimeTests
    {
        private TimeManagementDbContext dbContext;
        private ILeaveTimeRepository repository;

        private Guid worker1;
        private Guid worker2;
        private DbLeaveTime testLeaveTime1;
        private DbLeaveTime testLeaveTime2;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            var dbOptions = new DbContextOptionsBuilder<TimeManagementDbContext>()
                                    .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                                    .Options;
            dbContext = new TimeManagementDbContext(dbOptions);
            repository = new LeaveTimeRepository(dbContext);

            worker1 = Guid.NewGuid();
            worker2 = Guid.NewGuid();

            testLeaveTime1 = new DbLeaveTime
            {
                Id = Guid.NewGuid(),
                LeaveType = LeaveType.SickLeave,
                Comment = "SickLeave",
                StartTime = new DateTime(2020, 7, 5),
                EndTime = new DateTime(2020, 7, 25),
                WorkerUserId = worker1
            };
            testLeaveTime2 = new DbLeaveTime
            {
                Id = Guid.NewGuid(),
                LeaveType = LeaveType.Training,
                Comment = "SickLeave",
                StartTime = new DateTime(2020, 7, 10),
                EndTime = new DateTime(2020, 7, 20),
                WorkerUserId = worker2
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

        #region CreateLeaveTimeTests
        [Test]
        public void SuccessfulAddNewLeaveTimeInDb()
        {
            var guidOfNewLeaveTime = repository.CreateLeaveTime(testLeaveTime1);

            Assert.AreEqual(testLeaveTime1.Id, guidOfNewLeaveTime);
            Assert.NotNull(dbContext.LeaveTimes.Find(testLeaveTime1.Id));
        }
        #endregion

        #region GetUserLeaveTimesTests
        [Test]
        public void CorrectlyReturnsLeaveTime()
        {
            dbContext.Add(testLeaveTime1);
            dbContext.Add(testLeaveTime2);
            dbContext.SaveChanges();

            var leaveTimesOfWorker1 = repository.GetUserLeaveTimes(worker1);
            var leaveTimesOfWorker2 = repository.GetUserLeaveTimes(worker2);

            Assert.AreEqual(leaveTimesOfWorker1.Count, 1);
            Assert.IsTrue(leaveTimesOfWorker1.Contains(testLeaveTime1));

            Assert.AreEqual(leaveTimesOfWorker2.Count, 1);
            Assert.IsTrue(leaveTimesOfWorker2.Contains(testLeaveTime2));
        }
        #endregion
    }
}