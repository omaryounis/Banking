using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Shared.Models
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Status { get; set; }
        public T Data { get; set; }
        public IEnumerable<string> Errors { get; set; } = new List<string>();
        public Guid TrackingCorrelationId { get; set; } // For tracking requests
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }

}
