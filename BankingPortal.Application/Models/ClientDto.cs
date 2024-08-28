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
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MaxLength(60)]
        public string FirstName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        public string PersonalId { get; set; }

        [Required]
        public string MobileNumber { get; set; }  // Use a library for validating format

        public string ProfilePhoto { get; set; }

        [Required]
        public string Sex { get; set; } // Male, Female

        [Required]
        public AddressDto Address { get; set; }

        public List<AccountDto> Accounts { get; set; }
    }
}
