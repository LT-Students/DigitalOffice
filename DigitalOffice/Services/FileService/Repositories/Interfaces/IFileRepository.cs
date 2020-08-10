using LT.DigitalOffice.FileService.Database.Entities;
using System;

namespace LT.DigitalOffice.FileService.Repositories.Interfaces
{
    public interface IFileRepository
    {
        Guid AddNewFile(DbFile file);
        DbFile GetFileById(Guid fileId);
    }
}
