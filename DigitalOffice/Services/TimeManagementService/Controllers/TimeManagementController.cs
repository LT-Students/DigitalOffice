using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
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
