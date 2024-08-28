using BankingPortal.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities
{
    public class Client: IdentityBase
    {

        [Required, MaxLength(60)]
        public string FirstName { get; set; }

        [Required, MaxLength(60)]
        public string LastName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, StringLength(11, MinimumLength = 11)]
        public string PersonalId { get; set; }

        [Required]
        public string MobileNumber { get; set; } // Format validation to be handled elsewhere

        [Required]
        public string Sex { get; set; } // Male or Female

        public byte[] ProfilePhoto { get; set; } // Path to the profile photo

        // Navigation property for Address
        public Address Address { get; set; }

        // Navigation property for accounts - 1 client can have many accounts
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        // Additional metadata like created and modified dates (if needed)
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
