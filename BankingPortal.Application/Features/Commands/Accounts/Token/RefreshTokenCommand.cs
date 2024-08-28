using MediatR;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;

namespace BankPortal.Application.Features.Commands.Accounts.Token
{
    public class RefreshTokenCommand : IRequest<ResponseModel<AuthTokensDto>>
    {
        public string RefreshToken { get; set; }
    }
}
