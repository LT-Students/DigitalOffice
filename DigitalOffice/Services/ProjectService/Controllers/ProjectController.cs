using System;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;

namespace LT.DigitalOffice.ProjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        [Route("createNewProject")]
        [HttpPost]
        public Guid CreateNewProject(
            [FromServices] ICreateNewProjectCommand command,
            [FromBody] NewProjectRequest request) => command.Execute(request);
    }
}
