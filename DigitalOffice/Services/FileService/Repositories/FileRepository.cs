﻿using LT.DigitalOffice.FileService.Database;
using LT.DigitalOffice.FileService.Database.Entities;
using LT.DigitalOffice.FileService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.FileService.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly FileServiceDbContext dbContext;

        public FileRepository([FromServices] FileServiceDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Guid AddNewFile(DbFile file)
        {
            dbContext.Files.Add(file);
            dbContext.SaveChanges();
            
            return file.Id;
        }
    }
}
