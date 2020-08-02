using LT.DigitalOffice.FileService.Commands.Interfaces;
using LT.DigitalOffice.FileService.RestRequests;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.FileService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FileController : ControllerBase
    {
        [HttpPost("addNewFile")]
        public Guid AddNewFile(
            [FromBody] FileCreateRequest request,
            [FromServices] IAddNewFileCommand command)
        {
            return command.Execute(request);
        }
    }
}