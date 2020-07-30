using UserService.Database.Entities;

namespace UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool UserCreate(DbUser user);
    }
}
