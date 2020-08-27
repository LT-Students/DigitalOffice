using LT.DigitalOffice.UserService.Commands.Interfaces;
using LT.DigitalOffice.UserService.Database.Entities;
using LT.DigitalOffice.UserService.Mappers.Interfaces;
using LT.DigitalOffice.UserService.Models;
using LT.DigitalOffice.UserService.Repositories.Interfaces;
using System;

namespace LT.DigitalOffice.UserService.Commands
{
    /// <summary>
    /// Represents command class in command pattern. Provides method for getting user model by id.
    /// </summary>
    public class GetUserByIdCommand : IGetUserByIdCommand
    {
        private readonly IUserRepository repository;
        private readonly IMapper<DbUser, User> mapper;

        /// <summary>
        /// Initialize new instance of <see cref="GetUserByIdCommand"/> class with specified repository.
        /// </summary>
        /// <param name="repository">Specified repository.</param>
        /// <param name="mapper">Specified mapper that convert user model from database to user model for response.</param>
        public GetUserByIdCommand(IUserRepository repository, IMapper<DbUser, User> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Return user model by specified user's id from UserServiceDb.
        /// </summary>
        /// <param name="userId">Specified user's id.</param>
        /// <returns>User model with specified id.</returns>
        public User Execute(Guid userId)
            => mapper.Map(repository.GetUserInfoById(userId));
    }
}