﻿using System;
using LT.DigitalOffice.TimeManagementService.Database.Entities;
using LT.DigitalOffice.TimeManagementService.Mappers.Interfaces;
using LT.DigitalOffice.TimeManagementService.Models;

namespace LT.DigitalOffice.TimeManagementService.Mappers
{
    public class WorkTimeMapper : IMapper<CreateWorkTimeRequest, DbWorkTime>
    {
        public DbWorkTime Map(CreateWorkTimeRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            return new DbWorkTime
            {
                Id = Guid.NewGuid(),
                WorkerUserId = request.WorkerUserId,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Title = request.Title,
                ProjectId = request.ProjectId,
                Description = request.Description
            };
        }
    }
}
