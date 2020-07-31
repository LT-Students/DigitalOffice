using Microsoft.AspNetCore.Mvc;
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
