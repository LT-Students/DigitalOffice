using LT.DigitalOffice.CompanyService.Commands.Interfaces;
using LT.DigitalOffice.CompanyService.Database.Entities;
using LT.DigitalOffice.CompanyService.Mappers.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using LT.DigitalOffice.CompanyService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.CompanyService.Commands
{
    public class GetPositionByIdCommand : IGetPositionByIdCommand
    {
        private readonly ICompanyRepository repository;
        private readonly IMapper<DbPosition, Position> mapper;

        public GetPositionByIdCommand(
            [FromServices] ICompanyRepository repository,
            [FromServices] IMapper<DbPosition, Position> mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Position Execute(Guid positionId)
        {
            var dbPosition = repository.GetPositionById(positionId);
            return mapper.Map(dbPosition);
        }
    }
}