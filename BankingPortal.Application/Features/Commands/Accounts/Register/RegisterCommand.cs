using MediatR;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Enum;

namespace BankingPortal.Application.Features.Commands.Accounts.Register
{
    public class RegisterCommand : IRequest<ResponseModel<UserRegisterResponseDto>>
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string LastName { get; set; }
        public string LastNameAr { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }
        public string Code { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int [] RoleIds { get; set; }
    }
}
