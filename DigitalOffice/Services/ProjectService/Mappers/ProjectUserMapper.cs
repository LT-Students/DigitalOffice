using LT.DigitalOffice.ProjectService.Database.Entities;
using LT.DigitalOffice.ProjectService.Mappers.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using System;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class ProjectUserMapper : IMapper<AddUserToProjectRequest, DbProjectWorkerUser>
    {
        public DbProjectWorkerUser Map(AddUserToProjectRequest value)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            return new DbProjectWorkerUser
            {
                WorkerUserId = value.UserId,
                ProjectId = value.ProjectId,
                IsManager = value.IsManager
            };
        }
    }
}