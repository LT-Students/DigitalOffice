using LT.DigitalOffice.CompanyService.Commands.Interfaces;
using LT.DigitalOffice.CompanyService.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LT.DigitalOffice.CompanyService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        [HttpPost("addPosition")]
        public Guid AddPosition([FromServices] IAddPositionCommand command, [FromBody] AddPositionRequest request)
        {
            return command.Execute(request);
        }
        
        [HttpGet("getPositionById")]
        public Position GetPositionById([FromServices] IGetPositionByIdCommand command, [FromQuery] Guid positionId)
        {
            return command.Execute(positionId);
        }

        [HttpPost("addCompany")]
        public Guid AddCompany([FromServices] IAddCompanyCommand command, [FromBody] AddCompanyRequest request)
        {
            return command.Execute(request);
        }
    }
}