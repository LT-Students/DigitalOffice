using Microsoft.AspNetCore.Mvc;
using MassTransit;
using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Kernel.Broker;
using LT.DigitalOffice.ProjectService.Models;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;

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

        [HttpPut("editProjectById")]
        public Guid EditProjectById(
            [FromServices] IEditProjectByIdCommand command,
            [FromQuery] Guid projectId,
            [FromBody] EditProjectRequest request)
        {
            return command.Execute(projectId, request);
        }
    }
}