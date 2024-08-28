using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Infrastructure.Extensions.Middlewares.ExecutionContext
{
    public class IExecutionRequestContext
    {
        public Guid ApplicationSecretKey { get; set; }
        public Guid TrackingCorrelationId { get; set; }
        public int? UserId { get; set; }
        public string CurlCommand { get; set; }
    }
}
