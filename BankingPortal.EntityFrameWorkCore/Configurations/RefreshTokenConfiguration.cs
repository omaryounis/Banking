using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingPortal.Domain.Entities;
namespace BankingPortal.EntityFrameWorkCore.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("RefreshTokens", "user_management");
            builder.HasKey(s => s.Id); 
        }
    }
}