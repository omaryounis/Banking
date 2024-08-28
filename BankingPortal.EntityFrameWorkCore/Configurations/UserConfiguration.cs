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
                    Code= "1234",
                    Email="admin@gmail.com",
                    LastName="Admin",
                    LastNameAr=" نظام",
                    UserType= UserType.Admin,
                    Name = "Super",
                    NameAr= "مدير",
                    CreatedBy =1,
                    ModifiedBy =1,
                    CreatedDate=new DateTime(2024,07,10),
                    ModifiedDate=null,
                    Password = HashPassword("adminadmin", out byte[] salt1),
                    PasswordSalt = salt1
                },
                new User
                {
                    Id=2,
                    UserName = "user",
                    Code = "5678",
                    Email="user@gmail.com",
                    LastName="user",
                    LastNameAr="شركة",
                    UserType= UserType.User,
                    Name = "Tenant",
                    NameAr= "مدير",
                    CreatedBy =1,
                    ModifiedBy =1,
                    CreatedDate=new DateTime(2024,07,10),
                    ModifiedDate=null,
                    Password = HashPassword("TenantAdminPassword123", out byte[] salt2),
                    PasswordSalt = salt2

                },
                 new User
                {
                    Id=3,
                     UserName = "director",
                     Code = "5600",
                    Email="admin@hyyak.com",
                    LastName="Admin",
                    LastNameAr="مشروع",
                     UserType = UserType.User,
                    Name = "Project",
                    NameAr= "مدير",
                    CreatedBy =1,
                    ModifiedBy =1,
                    CreatedDate=new DateTime(2024,07,10),
                    ModifiedDate=null,
                     Password = HashPassword("useruser", out byte[] salt3),
                     PasswordSalt = salt3

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
