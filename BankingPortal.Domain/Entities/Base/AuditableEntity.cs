using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Domain.Entities.Base
{
    public abstract class AuditableEntity : Audit
    {

        public int Id { get; set; }
        public string Code { get; set; }
    }

}
