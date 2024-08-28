
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
            // Hash the password
            var (passwordHash, passwordSalt) = PasswordSecurity.CreatePasswordHash(request.Password);
            var user = new User
            {
                 Code =request.Code,
                 Email = request.Email,
                 LastName = request.LastName,
                 LastNameAr = request.LastNameAr,
                 Name = request.Name,
                 NameAr = request.NameAr,
                 Password = passwordHash,
                 PasswordSalt = passwordSalt,
                 UserType = request.UserType,
                 UserName = request.UserName,
            };
            await _unitOfWork.Repository<User>().AddAsync(user);
            foreach(var role in request.RoleIds)
            {
                var userRole = new UserRole() { RoleId = role, UserId = user.Id  };
                user.UserRoles.Add(userRole);
            }
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.CompleteAsync();
            return new ResponseModel<UserRegisterResponseDto>
            {
                IsSuccess = true,
                Status = "Success",
                Data = new UserRegisterResponseDto()
                { 
                    UserName=user.UserName
                }
            };
        }
    }


}