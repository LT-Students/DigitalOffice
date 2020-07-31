using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Models;

namespace UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public User GetUserByEmail(string email);
        public bool ContainsUserWithId(Guid id);
    }
}
