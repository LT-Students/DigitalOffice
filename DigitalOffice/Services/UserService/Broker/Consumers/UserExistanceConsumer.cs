using IntrnalModels;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Broker.Consumers
{
    public class UserExistanceConsumer : IConsumer<UserExistanceRequest>
    {
    }
}
