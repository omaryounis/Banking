using BankingPortal.Application.Models;
using BankingPortal.Domain.Enum;
using BankingPortal.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace BankingPortal.Application.Features.Commands.Clients
{
    public class CreateClientCommand : IRequest<ResponseModel<ClientDto>>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalId { get; set; }
        public string MobileNumber { get; set; }
        public SextType Sex { get; set; }
        public AddressDto Address { get; set; }
        public List<AccountDto> Accounts { get; set; } = new List<AccountDto>();

        // New property for the profile photo as a byte array
        public IFormFile ProfilePhoto { get; set; }
    }
}
