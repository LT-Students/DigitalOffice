using Microsoft.AspNetCore.Identity;
using ProjectService.Database.Entities;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        public bool AddUserToProject(DbProjectWorkerUser user, Guid project);
        public bool AddUserToProject(DbProjectManagerUser user, Guid project);
    }
}
