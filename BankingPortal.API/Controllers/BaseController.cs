using BankingPortal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace BankingPortal.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected ResponseModel<T> CreateResponse<T>(bool isSuccess, string status, T data, IEnumerable<string> errors = null)
        {
            return new ResponseModel<T>
            {
                IsSuccess = isSuccess,
                Status = status,
                Data = data,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
