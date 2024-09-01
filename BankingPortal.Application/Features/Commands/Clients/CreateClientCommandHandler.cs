using AutoMapper;
using BankingPortal.Application.Models;
using BankingPortal.Domain.Entities;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Infrastructure.Extensions.Middlewares.ExceptionHandling;
using BankingPortal.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Features.Commands.Clients
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ResponseModel<ClientDto>>
    {

        private readonly IGenericRepository<Client> _clientRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateClientCommandHandler(IGenericRepository<Client> clientRepository, IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _clientRepository = clientRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ResponseModel<ClientDto>> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // Validate that at least one account is present
            if (request.Accounts == null || !request.Accounts.Any())
                throw new UserCreationException("At least one account is required.");
            var client = _mapper.Map<Client>(request);
            client.SetProp(int.Parse(userId),DateTime.Now);
            // Save to the repository
            await _clientRepository.AddAsync(client);
            await _unitOfWork.CompleteAsync();

            var clientDto = _mapper.Map<ClientDto>(client);

            return new ResponseModel<ClientDto>
            {
                IsSuccess = true,
                Status = "Client created successfully.",
                Data = clientDto
            };
        }
    }
}