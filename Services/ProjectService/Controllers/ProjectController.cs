using System;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD:DigitalOffice/Services/ProjectService/Controllers/ProjectController.cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
=======
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
>>>>>>> develop:Services/ProjectService/Controllers/ProjectController.cs

namespace LT.DigitalOffice.ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [HttpGet("getProjectInfoById")]
        public Project GetProjectInfoById(
            [FromServices] IGetProjectInfoByIdCommand command,
            [FromQuery] Guid projectId)
        {
            return command.Execute(projectId);
        }

        [HttpPost("createNewProject")]
        public Guid CreateNewProject(
            [FromServices] ICreateNewProjectCommand command,
            [FromBody] NewProjectRequest request) => command.Execute(request);

<<<<<<< HEAD:DigitalOffice/Services/ProjectService/Controllers/ProjectController.cs
        [HttpPut("addUserToProject")]
        public async Task<IEnumerable<bool>> AddUserToProject(
            [FromBody] AddUserToProjectRequest request,
            [FromServices] IAddUsersToProjectCommand command)
        {
            return await command.Execute(request);
=======
        [HttpPut("editProjectById")]
        public Guid EditProjectById(
            [FromServices] IEditProjectByIdCommand command,
            [FromQuery] Guid projectId,
            [FromBody] EditProjectRequest request)
        {
            return command.Execute(projectId, request);
>>>>>>> develop:Services/ProjectService/Controllers/ProjectController.cs
        }
    }
}

