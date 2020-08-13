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
        /// Adds new company to the database. Returns the id of the added company.
        /// </summary>
        /// <param name="company">Company to add.</param>
        /// <returns>Id of the added company.</returns>
        Guid AddCompany(DbCompany company);
    }
}