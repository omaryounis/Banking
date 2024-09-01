using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Security.Cryptography;
using BankingPortal.Domain.Entities;
using BankingPortal.Domain.Enum;

namespace BankingPortal.EntityFrameWorkCore.Configurations
{

    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users", "user_management");
            builder.HasKey(s => s.Id);

            builder.HasData(
            [
                new User
                {
                    Id=1,
                    UserName="admin",
                    Password = HashPassword("P@ssw0rd", out byte[] salt1),
                    PasswordSalt = salt1,
                   
                },
                new User
                {
                    Id=2,
                    UserName = "user",
                    Password = HashPassword("P@ssw0rd", out byte[] salt2),
                    PasswordSalt = salt2,
                   
                }
            ]);
           
        }
        private static byte[] HashPassword(string password, out byte[] salt)
        {
            using var hmac = new HMACSHA512();
            salt = hmac.Key;
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }
    }
}
