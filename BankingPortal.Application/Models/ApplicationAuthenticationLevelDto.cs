using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Models
{
    public class ApplicationAuthenticationLevelDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Code { get; set; }
    }
}
