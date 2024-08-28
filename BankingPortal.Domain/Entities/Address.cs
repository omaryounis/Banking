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

        [Required, MaxLength(100)]
        public string Country { get; set; }

        [Required, MaxLength(100)]
        public string City { get; set; }

        [MaxLength(150)]
        public string Street { get; set; }

        [MaxLength(20)]
        public string ZipCode { get; set; }

        // Foreign key to Client entity
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }

}
