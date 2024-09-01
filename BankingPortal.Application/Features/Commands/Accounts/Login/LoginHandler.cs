
using MediatR;
using BankingPortal.Application.Features.Commands.Accounts.Register;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Infrastructure.Extensions.Helpers;
using BankingPortal.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace BankingPortal.Application.Features.Commands.Accounts.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResponseModel<UserResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenHelper _jwtHelper;
        private readonly IConfiguration _configuration;
        public LoginHandler(IUnitOfWork unitOfWork, JwtTokenHelper jwtHelper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
            _configuration = configuration;
        }

        public async Task<ResponseModel<UserResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var user = (await _unitOfWork.Repository<User>().IncludeAsync(u => u.UserName == request.UserName,i=>i.UserRoles)).FirstOrDefault();
            if (user == null || !PasswordSecurity.VerifyPasswordHash(request.Password, user.Password, user.PasswordSalt))
            {
                return new ResponseModel<UserResponseDto>
                {
                    IsSuccess = false,
                    Status = "Invalid credentials",
                    Timestamp = DateTime.UtcNow,
             
                };
            }
            // Generate JWT tokens
            var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
            var roles = (await _unitOfWork.Repository<Role>().GetAllAsync())
                .Where(x => roleIds.Contains(x.Id))
                .ToList();
            var (accessToken, refreshToken) = _jwtHelper.GenerateTokens(user.Id.ToString(), user.UserName,roles.Select(x => x.Name));
            var existRefreshToken = (await _unitOfWork.Repository<RefreshToken>().FirstOrDefaultAsync(x=>x.Token == refreshToken));
            var expirationDate = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["JwtSettings:RefreshTokenExpirationDays"]));
            if (existRefreshToken == null)
            {
                var token = RefreshToken.Create(refreshToken, user.Id, expirationDate);
                // Save the refresh token through the repository
                await _unitOfWork.Repository<RefreshToken>().AddAsync(token);
                await _unitOfWork.CompleteAsync();
            }


            return new ResponseModel<UserResponseDto>
            {
                IsSuccess = true,
                TrackingCorrelationId=user.TrackingCorrelationId,
                Status = "success",
                Timestamp = DateTime.UtcNow,
                Data=new UserResponseDto() { AccessToken =accessToken,RefreshToken=refreshToken, UserName=user.UserName}
            };
        }
    }


}