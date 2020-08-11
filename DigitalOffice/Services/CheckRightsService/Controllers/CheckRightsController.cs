using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using LT.DigitalOffice.CheckRightsService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.RestRequests;

namespace LT.DigitalOffice.CheckRightsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckRightsController : ControllerBase
    {
        [HttpGet("getRightsList")]
        public List<Right> GetRightsList([FromServices] IGetRightsListCommand command)
        {
            return command.Execute();
        }

        [HttpPost("addRightsForUser")]
        public bool AddRightsForUser(
            [FromServices] IAddRightsForUserCommand command,
            [FromBody] RightsForUserRequest request)
        {
            return command.Execute(request);
        }
    }
}