using LT.DigitalOffice.ProjectService.Database.Entities;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using System;

namespace LT.DigitalOffice.ProjectService.Mappers
{
    public class ProjectUserMapper : IMapper<AddUserToProjectRequest, DbProjectWorkerUser>
    {
        public DbProjectWorkerUser Map(AddUserToProjectRequest value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
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