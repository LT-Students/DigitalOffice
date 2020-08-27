﻿using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using System;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class ProjectUserMapper : IMapper<ProjectUser, DbProjectWorkerUser>
    {
        public DbProjectWorkerUser Map(ProjectUser value)
        {
            return new DbProjectWorkerUser
            {
                WorkerUserId = value.UserId,
                ProjectId = value.ProjectId,
                IsManager = value.IsManager
            };
        }
    }
}