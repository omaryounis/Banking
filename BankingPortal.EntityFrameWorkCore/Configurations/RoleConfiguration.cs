using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingPortal.Domain.Entities;

namespace BankingPortal.EntityFrameWorkCore.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles", "user_management");
            builder.HasKey(s => s.Id);
            builder.HasData(
                    [
                        new Role
                         {
                             Id = 1,
                             Name = "admin",
                             NameAr = "مدير النظام",

                         },
                        new Role
                        {
                            Id = 2,
                            Name = "user",
                            NameAr = "مستخدم",

                        }
                    ]);
        }
    }
}