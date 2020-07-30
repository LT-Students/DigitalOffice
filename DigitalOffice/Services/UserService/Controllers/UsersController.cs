using Microsoft.AspNetCore.Mvc;
using UserService.Commands.Interfaces;
using UserService.RestRequests;

namespace UserService.Controllers
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
