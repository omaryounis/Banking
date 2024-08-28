using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Infrastructure.Extensions.Middlewares.ExceptionHandling
{
    public class UserCreationException : Exception
    {
        public UserCreationException(string message) : base(message) { }
    }

}
