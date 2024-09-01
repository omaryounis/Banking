using BankingPortal.Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }  // Unique account number for the bank account
        public decimal Balance { get; set; }  // Account balance
        public AccountType AccountType { get; set; }  // Enum to specify account type (Savings, Checking, etc.)
        // Foreign key to Client entity
        public int ClientId { get; set; }
        public Client Client { get; set; }  // Navigation property to the Client entity
    }

}
