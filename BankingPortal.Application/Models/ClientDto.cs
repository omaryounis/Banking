using BankingPortal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Models
{
    public class ClientDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string PersonalId { get; set; }
        public string MobileNumber { get; set; }  // Use a library for validating format

        public byte[] ProfilePhoto { get; set; }
        public SextType Sex { get; set; } // Male, Female

        //public AddressDto Address { get; set; }

        //public List<AccountDto> Accounts { get; set; }
    }
}
