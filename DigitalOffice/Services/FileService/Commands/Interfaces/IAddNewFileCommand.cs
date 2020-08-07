using LT.DigitalOffice.FileService.RestRequests;
using System;

namespace LT.DigitalOffice.FileService.Commands.Interfaces
{
    public interface IAddNewFileCommand
    {
        Guid Execute(FileCreateRequest request);
    }
}
