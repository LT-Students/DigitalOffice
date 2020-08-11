using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using ProjectService.Models;
using ProjectService.Commands.Interfaces;

namespace LT.DigitalOffice.ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet("getProjectInfoById")]
        public Project GetProjectInfoById([FromServices] IGetProjectInfoByIdCommand command, [FromQuery] Guid projectId)
        {
            return command.Execute(projectId);
        }

        [HttpPost("createNewProject")]
        public Guid CreateNewProject(
            [FromServices] ICreateNewProjectCommand command,
            [FromBody] NewProjectRequest request) => command.Execute(request);

        [HttpPut]
        public bool AdduserToProject(
            [FromBody] AddUserToProjectRequest request,
            [FromServices] IAddUserToProjectCommand command)
        {
            return command.Execute(request);
        }
    }
}

