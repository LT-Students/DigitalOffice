using LT.DigitalOffice.AuthenticationService.Commands.Interfaces;
using LT.DigitalOffice.AuthenticationService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LT.DigitalOffice.AuthenticationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController
    {
        [HttpPost("login")]
        public async Task<UserLoginResult> LoginUser(
                [FromServices] IUserLoginCommand command,
                [FromBody] UserLoginInfoRequest userCredentials)
        {
            return await command.Execute(userCredentials);
        }
    }
}