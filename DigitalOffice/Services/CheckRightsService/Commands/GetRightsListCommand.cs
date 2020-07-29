using CheckRightsService.Commands.Interfaces;
using System.Collections.Generic;
using CheckRightsService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CheckRightsService.Models;

namespace CheckRightsService.Commands
{
    public class GetRightsListCommand : IGetRightsListCommand
    {
        private readonly ICheckRightsRepository repository;

        public GetRightsListCommand([FromServices] ICheckRightsRepository repository)
        {
            this.repository = repository;
        }

        public List<Right> Execute()
        {
            return repository.GetRightsList();
        }
    }
}