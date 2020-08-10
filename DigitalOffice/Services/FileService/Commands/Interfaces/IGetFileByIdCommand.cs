using LT.DigitalOffice.FileService.Models;
using System;

namespace LT.DigitalOffice.FileService.Commands.Interfaces
{
    public interface IGetFileByIdCommand
    {
        File Execute(Guid fileId);
    }
}
