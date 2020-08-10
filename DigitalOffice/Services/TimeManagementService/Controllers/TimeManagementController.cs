using Microsoft.AspNetCore.Mvc;
using System;
using LT.DigitalOffice.TimeManagementService.Commands.Interfaces;
using LT.DigitalOffice.TimeManagementService.Models;

namespace LT.DigitalOffice.TimeManagementService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeManagementController : ControllerBase
    {
        [Route("AddWorkTime")]
        [HttpPost]
        public Guid AddWorkTime(
            [FromBody] CreateWorkTimeRequest workTime,
            [FromServices] ICreateWorkTimeCommand command)
        {
            return command.Execute(workTime);
        }
    }
}
