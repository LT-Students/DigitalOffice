﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LT.DigitalOffice.UserService.Broker.Responses
{
    public interface IUserExistenceResponse
    {
        bool Exists { get; }
    }
}
