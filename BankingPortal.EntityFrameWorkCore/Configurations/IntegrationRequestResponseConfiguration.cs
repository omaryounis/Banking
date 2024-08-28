using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BankingPortal.Domain.Entities;

namespace BankingPortal.EntityFrameWorkCore.Configurations
{

    public class IntegrationRequestResponseConfiguration : IEntityTypeConfiguration<IntegrationRequestResponse>
    {
        public void Configure(EntityTypeBuilder<IntegrationRequestResponse> builder)
        {
            builder.ToTable("IntegrationRequestResponse", "log");
            builder.HasKey(s => s.Id);
        }
    }
}
