using AutoMapper;
using BankingPortal.Application.Models;
using BankingPortal.Domain.Entities;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Shared.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Features.Queries.Clients
{
    public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, PagedResult<ClientDto>>
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetClientsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<PagedResult<ClientDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
        {
            // Filtering, sorting, and paging logic
            var filter = string.IsNullOrEmpty(request.SearchTerm)
                ? null
                : (Expression<Func<Client, bool>>)
                (c => 
                c.FirstName.Contains(request.SearchTerm) || 
                c.LastName.Contains(request.SearchTerm )||
                c.Email == request.SearchTerm ||
                c.MobileNumber==request.SearchTerm ||
                c.PersonalId==request.SearchTerm );

            Func<IQueryable<Client>, IOrderedQueryable<Client>> orderBy = !request.SortDescending
                ? q => q.OrderBy(e => EF.Property<object>(e, request.SortBy))
                : q => q.OrderByDescending(e => EF.Property<object>(e, request.SortBy));


            // Fetch clients using the repository
            var clients = await _unitOfWork.Repository<Client>().GetPagedAsync(
                pageIndex: request.PageIndex,
                pageSize: request.PageSize,
                filter: filter,
                orderBy: orderBy,
                includes: [x => x.Address, x => x.Accounts]
            );

            // Map to List<ClientDto>
            var clientDtos = _mapper.Map<List<ClientDto>>(clients.Items);

            return new PagedResult<ClientDto>
            {
                Items = clientDtos,
                TotalCount = clients.TotalCount,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
        }
    }

}
