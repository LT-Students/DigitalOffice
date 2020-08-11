using System;

namespace LT.DigitalOffice.UserService.Broker.Requests
{
    public class UserExistenceRequest : IUserExistenceRequest
    {
        public Guid Id { get; }
    }
}
