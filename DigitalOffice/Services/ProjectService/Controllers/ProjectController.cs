using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using ProjectService.Commands.Interfaces;
using ProjectService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectController : ControllerBase
    {
        [HttpPut]
        public bool AdduserToProject(
            [FromBody] AddUserToProjectRequest request,
            [FromServices] IAddUserToProjectCommand command)
        {
            return command.Execute(request);
        }
    }
}
=======
using LT.DigitalOffice.ProjectService.Commands.Interfaces;
using LT.DigitalOffice.ProjectService.Models;
using System;

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
    }
}
>>>>>>> develop
