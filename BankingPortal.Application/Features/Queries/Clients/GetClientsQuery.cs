using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Features.Queries.Clients
{
    public class GetClientsQuery : IRequest<PagedResult<ClientDto>>
    {
        public string? SearchTerm { get; set; } = null;// For filtering
        public int PageIndex { get; set; } = 1; // For paging
        public int PageSize { get; set; } = 10; // For paging
        public string SortBy { get; set; } = "Id"; // Sorting field
        public bool SortDescending { get; set; } = true; // Sort direction
    }


}
