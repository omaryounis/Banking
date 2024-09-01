using MediatR;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Enum;

namespace BankingPortal.Application.Features.Commands.Accounts.Register
{
    public class RegisterCommand : IRequest<ResponseModel<UserRegisterResponseDto>>
    {
        public string UserName { get; set; } 
        public string Password { get; set; }
        public int [] RoleIds { get; set; }
    }
}
