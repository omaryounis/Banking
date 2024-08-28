using BankingPortal.Domain.Entities;
using BankingPortal.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.Application.Features.Commands.Clients
{
    public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, int>
    {

        private readonly IUnitOfWork _unitOfWork;

        public CreateClientCommandHandler( IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = new Client
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PersonalId = request.PersonalId,
                MobileNumber = request.MobileNumber,
                Sex = request.Sex,
                ProfilePhoto = request.ProfilePhoto,
                Address = new Address
                {
                    Country = request.Address.Country,
                    City = request.Address.City,
                    Street = request.Address.Street,
                    ZipCode = request.Address.ZipCode
                },
                Accounts = request.Accounts.Select(a => new Account { AccountNumber = a.AccountNumber }).ToList()
            };

            await _unitOfWork.Repository<Client>().AddAsync(client);
            await _unitOfWork.CompleteAsync();
            return client.Id;
        }
    }

}
