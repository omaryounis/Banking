
using MediatR;
using BankingPortal.Application.Features.Commands.Accounts.Register;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Infrastructure.Extensions.Helpers;
using BankingPortal.Domain.Entities;

namespace BankingPortal.Application.Features.Commands.Accounts.Login
{
    public class LoginHandler : IRequestHandler<LoginCommand, ResponseModel<UserResponseDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtTokenHelper _jwtHelper;
        public LoginHandler(IUnitOfWork unitOfWork, JwtTokenHelper jwtHelper)
        {
            _unitOfWork = unitOfWork;
            _jwtHelper = jwtHelper;
        }

        public async Task<ResponseModel<UserResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            var user = (await _unitOfWork.Repository<User>().IncludeAsync(u => u.Email == request.Email,i=>i.UserRoles)).FirstOrDefault();
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
            var (accessToken, refreshToken) = _jwtHelper.GenerateTokens(user.Id.ToString(), user.UserName, user.Email,roles.Select(x => x.Name));
           
            return new ResponseModel<UserResponseDto>
            {
                IsSuccess = true,
                Status = "success",
                Timestamp = DateTime.UtcNow,
                Data=new UserResponseDto() { AccessToken =accessToken,RefreshToken=refreshToken, UserName=user.UserName}
            };
        }
    }


}