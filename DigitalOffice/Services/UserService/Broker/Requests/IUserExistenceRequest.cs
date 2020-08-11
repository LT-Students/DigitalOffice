using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Broker.Requests
{
    public interface IUserExistenceRequest
    {
        Guid Id { get; }
    }
}
