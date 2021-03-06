﻿using LT.DigitalOffice.FileService.Database.Entities;
using System;

namespace LT.DigitalOffice.FileService.Repositories.Interfaces
{
    /// <summary>
    /// Represents interface of repository in repository pattern.
    /// Provides methods for working with the database of FileService.
    /// </summary>
    public interface IFileRepository
    {
        /// <summary>
        /// Adds new file to the database. Returns the id of the added file.
        /// </summary>
        /// <param name="file">File to add.</param>
        /// <returns>Id of the added file.</returns>
        Guid AddNewFile(DbFile file);

        /// <summary>
        /// Returns the file with the specified id from database.
        /// </summary>
        /// <param name="fileId">Specified id of file.</param>
        /// <returns>File with specified id.</returns>
        DbFile GetFileById(Guid fileId);
    }
}
