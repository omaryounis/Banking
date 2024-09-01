using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities
{
    public class Address
    {
        public int Id { get; set; }
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string ZipCode { get; set; }

        // Foreign key to Client entity
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
