using BankingPortal.Application.Features.Commands.Accounts.Login;
using BankingPortal.Application.Features.Commands.Accounts.Register;
using BankPortal.Application.Features.Commands.Accounts.Token;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/account")]
    public class AccountController : BaseController
    {
        private readonly MediatR.IMediator _mediator;
        public AccountController(MediatR.IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand loginCommand)
        {
            var response = await _mediator.Send(loginCommand);
            return Ok(response);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand registerCommand)
        {
            var response = await _mediator.Send(registerCommand);
            return Ok(response);
        }
        [HttpPost("refresh-token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenCommand refreshTokenCommand)
        {
            var response = await _mediator.Send(refreshTokenCommand);
            return Ok(response);
        }
    }
}
