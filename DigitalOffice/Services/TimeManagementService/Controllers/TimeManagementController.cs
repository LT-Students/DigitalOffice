using LT.DigitalOffice.TimeManagementService.Commands.Interfaces;
using LT.DigitalOffice.TimeManagementService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.TimeManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeManagementController : ControllerBase
    {
        [HttpPost("addWorkTime")]
        public Guid AddWorkTime(
            [FromBody] CreateWorkTimeRequest workTime,
            [FromServices] ICreateWorkTimeCommand command)
        {
            return command.Execute(workTime);
        }
    }
}
