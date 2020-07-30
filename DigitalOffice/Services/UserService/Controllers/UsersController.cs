using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.RestRequests;

namespace LT.DigitalOffice.UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [Route("register")]
        [HttpPost]
        public bool CreateUser([FromServices] IUserCreateCommand command, [FromBody] UserCreateRequest request)
        {
            return command.Execute(request);
        }
    }
}
