using LT.DigitalOffice.CompanyService.Commands.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

namespace LT.DigitalOffice.CompanyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpGet("getPositionsList")]
        public List<Position> GetPositionsList([FromServices] IGetPositionsListCommand command)
        {
            return command.Execute();
        }

        [HttpPost("getCompanyById")]
        public Company GetCompanyById([FromServices] IGetCompanyByIdCommand command, [FromQuery] Guid companyId)
        {
            return command.Execute(companyId);
        }

        [HttpPost("addCompany")]
        public Guid AddCompany([FromServices] IAddCompanyCommand command, [FromBody] AddCompanyRequest request)
        {
            return command.Execute(request);
        }

        [HttpPost("changeCompany")]
        public bool ChangeCompany([FromServices] IEditCompanyCommand command, [FromBody] EditCompanyRequest request)
        {
            return command.Execute(request);
        }

        [HttpGet("getPositionById")]
        public Position GetPositionById([FromServices] IGetPositionByIdCommand command, [FromQuery] Guid positionId)
        {
            return command.Execute(positionId);
        }

        [HttpPost("addPosition")]
        public Guid AddPosition([FromServices] IAddPositionCommand command, [FromBody] AddPositionRequest request)
        {
            return command.Execute(request);
        }

        [HttpPost("editPosition")]
        public bool EditPosition([FromServices] IEditPositionCommand command, [FromBody] EditPositionRequest request)
        {
            return command.Execute(request);
        }
    }
}