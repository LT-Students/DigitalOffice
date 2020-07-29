using CheckRightsService.Commands.Interfaces;
using CheckRightsService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CheckRightsService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckRightsController : ControllerBase
    {
        [Route("GetRightsList")]
        [HttpGet]
        public List<Right> GetRightsList([FromServices] IGetRightsListCommand command)
        {
            return command.Execute();
        }
    }
}
