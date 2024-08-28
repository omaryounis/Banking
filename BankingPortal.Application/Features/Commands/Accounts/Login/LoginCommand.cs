using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Features.Commands.Accounts.Login
{
    public class LoginCommand : IRequest<ResponseModel<UserResponseDto>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
