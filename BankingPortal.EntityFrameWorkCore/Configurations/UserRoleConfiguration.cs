using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingPortal.Domain.Entities;

namespace BankingPortal.EntityFrameWorkCore.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("UserRoles", "user_management");
            builder.HasKey(s => s.Id);

            // Configure the foreign key relationship with cascading delete
            builder.HasOne(ur => ur.User)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.UserId);

            // Configure the foreign key relationship with cascading delete
            builder.HasOne(ur => ur.Role)
                   .WithMany(u => u.UserRoles)
                   .HasForeignKey(ur => ur.RoleId);

        }
    }
}