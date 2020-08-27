using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpPut("addUserToProject")]
        public async Task<IEnumerable<bool>> AddUserToProject(
            [FromBody] AddUserToProjectRequest request,
            [FromServices] IAddUsersToProjectCommand command)
        {
            return await command.Execute(request);
        }
    }
}

