using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities
{
    public class IntegrationRequestResponse
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
    }
}
