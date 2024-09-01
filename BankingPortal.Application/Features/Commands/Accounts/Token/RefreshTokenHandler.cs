using MediatR;
using Microsoft.Extensions.Configuration;
using BankingPortal.Application.Models;
using BankingPortal.Shared.Models;
using BankingPortal.Domain.Interfaces;
using BankingPortal.Infrastructure.Extensions.Helpers;
using BankingPortal.Domain.Entities;
using BankingPortal.Infrastructure.Extensions.Middlewares.ExceptionHandling;
namespace BankPortal.Application.Features.Commands.Accounts.Token
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenCommand, ResponseModel<AuthTokensDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _configuration;
        private readonly JwtTokenHelper _jwtTokenHelper;
        public RefreshTokenHandler(IUnitOfWork unitOfWork, IConfiguration configuration, JwtTokenHelper jwtTokenHelper)
        {
            _unitOfWork = unitOfWork;
            _configuration = configuration;
            _jwtTokenHelper = jwtTokenHelper;
        }
        public async Task<ResponseModel<AuthTokensDto>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var refreshToken = await _unitOfWork.Repository<RefreshToken>().FindAsync(x=>x.Token == request.RefreshToken);
            if (refreshToken == null || refreshToken.Expiration <= DateTime.UtcNow)
            {
                throw new UserCreationException("Invalid or expired refresh token");
            }

            var user = await _unitOfWork.Repository<User>().GetByIdAsync(refreshToken.UserId,includes:i=>i.UserRoles);
            if (user == null)
            {
                throw new UserCreationException("User not found");
            }
            var roleIds = user.UserRoles.Select(ur => ur.RoleId).ToList();
            var roles = (await _unitOfWork.Repository<Role>().GetAllAsync())
                .Where(x => roleIds.Contains(x.Id))
                .ToList();
            var (newAccessToken, newRefreshToken) = _jwtTokenHelper.GenerateTokens(user.Id.ToString(), user.UserName, roles.Select(x => x.Name));
            refreshToken.Token = newRefreshToken;

            refreshToken.Expiration = DateTime.UtcNow.AddDays(Convert.ToInt32(_configuration["JwtSettings:RefreshTokenExpirationDays"]));
            await _unitOfWork.Repository<RefreshToken>().UpdateAsync(refreshToken);
            var responseModel = new AuthTokensDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken
            };
            return new ResponseModel<AuthTokensDto> {Data= responseModel,IsSuccess=true,Status="Success" };
        }
    }
}
