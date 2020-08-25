using LT.DigitalOffice.CompanyService.Models;
﻿using LT.DigitalOffice.CompanyService.Database.Entities;
using System;
using System.Collections.Generic;

namespace LT.DigitalOffice.CompanyService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of CompanyService.
    /// </summary>
    public interface ICompanyRepository
    {
        #region Company
        /// <summary>
        /// Returns the company with the specified id from database.
        /// </summary>
        /// <param name="companyId">Specified id of company.</param>
        /// <returns>Company with specified id.</returns>
        DbCompany GetCompanyById(Guid companyId);

        /// <summary>
        /// Adds new company to the database. Returns the id of the added company.
        /// </summary>
        /// <param name="company">Company to add.</param>
        /// <returns>Id of the added company.</returns>
        Guid AddCompany(DbCompany company);

        /// <summary>
        /// Trying to update the company. Returns whether an update exited.
        /// </summary>
        /// <param name="company">Edited company model.</param>
        /// <returns>true if the company is up to date. Otherwise false.</returns>
        bool UpdateCompany(DbCompany company);
        #endregion

        #region Position
        /// <summary>
        /// Returns the position with the specified id from database.
        /// </summary>
        /// <param name="positionId">Specified id of position.</param>
        /// <returns>Position with specified id.</returns>
        DbPosition GetPositionById(Guid positionId);

        /// <summary>
        /// Returns a list of all added positions to the database.
        /// </summary>
        /// <returns>List of all added positions.</returns>
        List<DbPosition> GetPositionsList();

        /// <summary>
        /// Adds new position to the database. Returns its Id.
        /// </summary>
        /// <param name="position">Position to add.</param>
        /// <returns>New position Id.</returns>
        Guid AddPosition(DbPosition position);

        /// <summary>
        /// Edits an existing position in the database. Returns whether it was successful to edit.
        /// </summary>
        /// <param name="position">Position to edit.</param>
        /// <returns>Whether it was successful to edit.</returns>
        bool EditPosition(DbPosition position);
        #endregion
    }
}