using LT.DigitalOffice.UserService.Database.Entities;

namespace LT.DigitalOffice.UserService.Repositories.Interfaces
{
    public interface IUserRepository
    {
        bool UserCreate(DbUser user);
    }
}
