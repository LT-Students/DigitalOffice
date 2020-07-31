using ProjectService.Database.Entities;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Mappers
{
    public class WorkerMapper : IMapper<AddUserToProjectRequest, DbProjectWorkerUser>
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
                ProjectId = value.ProjectId
            };
        }
    }
}