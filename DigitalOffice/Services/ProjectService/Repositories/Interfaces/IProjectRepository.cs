using System;
using LT.DigitalOffice.ProjectService.Database.Entities;

namespace LT.DigitalOffice.ProjectService.Repositories.Interfaces
{
    public interface IProjectRepository
    {
        DbProject GetProjectInfoById(Guid projectId);
        Guid CreateNewProject(DbProject item);
    }
}
