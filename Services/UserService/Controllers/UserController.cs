using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet("getUserById")]
        public User GetUserById([FromServices] IGetUserByIdCommand getUserInfoByIdCommand, [FromQuery] Guid userId)
            => getUserInfoByIdCommand.Execute(userId);

        [HttpPost("register")]
        public Guid CreateUser([FromServices] IUserCreateCommand command, [FromBody] UserCreateRequest request)
        {
            return command.Execute(request);
        }

        [HttpPost("editUser")]
        public bool EditUser([FromServices] IEditUserCommand command, [FromBody] EditUserRequest request)
        {
            return command.Execute(request);
        }

        [HttpGet("getUserByEmail")]
        public User GetUserByEmail([FromServices] IGetUserByEmailCommand command, [FromQuery] string userEmail)
        {
            return command.Execute(userEmail);
        }
    }
}