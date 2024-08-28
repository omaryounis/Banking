using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities.Base
{
    public class IdentityBase:Audit
    {
        public int Id { get; set; }
    }
}
