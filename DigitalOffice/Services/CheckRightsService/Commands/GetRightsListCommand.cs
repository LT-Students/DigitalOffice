﻿using LT.DigitalOffice.CheckRightsService.Commands.Interfaces;
using System.Collections.Generic;
using LT.DigitalOffice.CheckRightsService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using LT.DigitalOffice.CheckRightsService.Models;

namespace LT.DigitalOffice.CheckRightsService.Commands
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