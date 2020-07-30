using System;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.RestRequests;
using Microsoft.AspNetCore.Mvc;

namespace LT.DigitalOffice.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("getUserById")]
        [HttpGet]
        public User GetUserById([FromServices] IGetUserByIdCommand getUserInfoByIdCommand, [FromQuery] Guid userId)
            => getUserInfoByIdCommand.Execute(userId);
        
        [Route("register")]
        [HttpPost]
        public bool CreateUser([FromServices] IUserCreateCommand command, [FromBody] UserCreateRequest request)
        {
            return command.Execute(request);
        }
    }
}