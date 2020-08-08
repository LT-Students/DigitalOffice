﻿using System;
using System.Threading.Tasks;
using LT.DigitalOffice.Broker.Requests;
using LT.DigitalOffice.Kernel.Broker;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.TimeManagementService.Controllers
{
    [Route("[controller]")]
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
    }
}
