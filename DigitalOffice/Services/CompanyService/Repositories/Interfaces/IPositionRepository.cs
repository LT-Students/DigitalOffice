using System;
using LT.DigitalOffice.CompanyService.Database.Entities;

namespace LT.DigitalOffice.CompanyService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of PositionService.
    /// </summary>
    public interface IPositionRepository
    {
        /// <summary>
        /// Returns the position of user.
        /// </summary>
        /// <param name="userId">Specified id of user.</param>
        /// <returns>User's position.</returns>
        DbPosition GetUserPosition(Guid userId);
    }
}