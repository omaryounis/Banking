
using MediatR;
using Microsoft.Extensions.Configuration;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Infrastructure.Extensions.Helpers;
using BankingPortal.Domain.Entities;

namespace BankingPortal.Application.Features.Commands.Accounts.Register
{
    public class RegisterHandler : IRequestHandler<RegisterCommand, ResponseModel<UserRegisterResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenHelper _jwtTokenHelper;

        private readonly IConfiguration _configuration;
        public RegisterHandler(IUnitOfWork unitOfWork, JwtTokenHelper jwtTokenHelper, IConfiguration configuration = null)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenHelper = jwtTokenHelper;
            _configuration = configuration;
        }

        public async Task<ResponseModel<UserRegisterResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var trackingCorrelationId= Guid.NewGuid();
            // Hash the password
            var (passwordHash, passwordSalt) = PasswordSecurity.CreatePasswordHash(request.Password);
            // Business logic to create a user is in the User domain entity
            var user = User.Create(request.UserName, passwordHash, passwordSalt, trackingCorrelationId);

            // Assign roles to the user inside the aggregate
            foreach (var roleId in request.RoleIds)
            {
                user.AssignRole(roleId);
            }
            // Save the user through the repository
            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.CompleteAsync();
            return new ResponseModel<UserRegisterResponseDto>
            {
                IsSuccess = true,
                Status = "Success",
                TrackingCorrelationId = trackingCorrelationId,
                Data = new UserRegisterResponseDto()
                { 
                    UserName=user.UserName
                }
            };
        }
    }


}