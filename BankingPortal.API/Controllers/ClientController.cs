using BankingPortal.Application.Features.Commands.Clients;
using BankingPortal.Application.Features.Queries.Clients;
using BankingPortal.Application.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/client")]
    public class ClientController : BaseController
    {
        private readonly MediatR.IMediator _mediator;
        public ClientController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/client
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetClients([FromQuery] GetClientsQuery request)
        {
            var result = await _mediator.Send(request);
            return Ok(result);
        }


        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateClient([FromForm] CreateClientCommand command)
        {
            var client = await _mediator.Send(command);
            return Ok(client);
        }

    }
}
