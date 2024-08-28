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
        }
    }
}