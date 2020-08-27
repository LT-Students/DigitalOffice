using System;

namespace LT.DigitalOffice.AuthenticationService.Models
{
    public class UserLoginResult
    {
        public Guid UserId { get; set; }
        public string JwtToken { get; set; }
    }
}