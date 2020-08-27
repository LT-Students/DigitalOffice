using LT.DigitalOffice.ProjectService.Database.Entities;
using System;

namespace LT.DigitalOffice.ProjectService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of ProjectService.
    /// </summary>
    public interface IProjectRepository
    {
        /// <summary>
        /// Returns the project with the specified id from database.
        /// </summary>
        /// <param name="projectId">Specified id of project.</param>
        /// <returns>Project with specified id.</returns>
        DbProject GetProjectInfoById(Guid projectId);

        /// <summary>
        /// Adds new project to the database. Returns the id of the added project.
        /// </summary>
        /// <param name="item">Project to add.</param>
        /// <returns>Id of the added project.</returns>
        Guid CreateNewProject(DbProject item);

        /// <summary>
        /// Adds new user to the project. Returns whether the operation was successful or not.
        /// </summary>
        /// <param name="user">User to add.</param>
        /// <param name="projectId">Project to whcih the user is added.</param>
        /// <returns>Result of the operation.</returns>
        bool AddUserToProject(DbProjectWorkerUser user, Guid projectId);
       
        /// <summary>
        /// Edits the existing project in the database.
        /// </summary>
        /// <param name="dbProject">New data of the project.</param>
        /// <returns>Id of the edited project.</returns>
        Guid EditProjectById(DbProject dbProject);
    }
}