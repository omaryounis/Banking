using BankingPortal.Domain.Entities.Base;
using BankingPortal.Domain.Enum;
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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PersonalId { get; set; }

        public string MobileNumber { get; set; } 
        public SextType Sex { get; set; } // Male or Female

        public byte[] ProfilePhoto { get; set; } // Path to the profile photo

        // Navigation property for Address
        public Address Address { get; set; }

        // Navigation property for accounts - 1 client can have many accounts
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

        public void SetProp(int userId,DateTime createdDate)
        {
            CreatedBy=userId;
            CreatedDate=createdDate;
        }

    }

}
