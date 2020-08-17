using LT.DigitalOffice.CompanyService.Database.Entities;
using System;

namespace LT.DigitalOffice.CompanyService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of CompanyService.
    /// </summary>
    public interface ICompanyRepository
    {
        /// <summary>
        /// Returns the position with the specified id from database.
        /// </summary>
        /// <param name="positionId">Specified id of position.</param>
        /// <returns>Position with specified id.</returns>
        DbPosition GetPositionById(Guid positionId);

        /// <summary>
        /// Adds new company to the database. Returns the id of the added company.
        /// </summary>
        /// <param name="company">Company to add.</param>
        /// <returns>Id of the added company.</returns>
        Guid AddCompany(DbCompany company);
    }
}