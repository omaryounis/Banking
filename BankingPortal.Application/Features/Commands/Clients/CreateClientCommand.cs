using BankingPortal.Application.Models;
using MediatR;
using System.Collections.Generic;

namespace BankingPortal.Application.Features.Commands.Clients
{
    public class CreateClientCommand : IRequest<int> // Return client ID
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PersonalId { get; set; }
        public string MobileNumber { get; set; }
        public string Sex { get; set; }
        public AddressDto Address { get; set; }
        public List<AccountDto> Accounts { get; set; }

        // New property for the profile photo as a byte array
        public byte[] ProfilePhoto { get; set; }
    }
}
