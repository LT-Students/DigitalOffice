using ProjectService.Database.Entities;
using ProjectService.Mappers.Interfaces;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Mappers
{
    public class ManagerMapper : IMapper<AddUserToProjectRequest, DbProjectManagerUser>
    {
        public DbProjectManagerUser Map(AddUserToProjectRequest value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }

            return new DbProjectManagerUser
            {
                ManagerUserId = value.UserId,
                ProjectId = value.ProjectId
            };
        }
    }
}