using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;

namespace LT.DigitalOffice.ProjectService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        [Route("getProjectInfoById")]
        [HttpGet]
        public Project GetProjectInfoById([FromServices] IGetProjectInfoByIdCommand command, [FromQuery] Guid projectId)
        {
            return command.Execute(projectId);
        }

        [Route("createNewProject")]
        [HttpPost]
        public Guid CreateNewProject(
            [FromServices] ICreateNewProjectCommand command,
            [FromBody] NewProjectRequest request) => command.Execute(request);

        [HttpGet("checkIfUserHaveRight")]
        public async Task<bool> CheckIfUserHaveRight(
            [FromServices] IRequestClient<ICheckIfUserHaveRightRequest> requestClient, [FromQuery] int rightId,
            [FromQuery] Guid userId)
        {
            var response = await requestClient.GetResponse<IOperationResult<bool>>(new
            {
                UserId = userId,
                RightId = rightId
            });
            if (!response.Message.IsSuccess)
            {
                throw new Exception("Operation result is not success.");
            }

            return response.Message.Body;
        }
    }
}