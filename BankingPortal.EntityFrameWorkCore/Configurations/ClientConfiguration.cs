using BankingPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingPortal.EntityFrameWorkCore.Configurations
{
    public class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("Clients", "dbo");
            builder.HasKey(s => s.Id);

            // Configuring properties with constraints
            builder.Property(s => s.Email)
                .IsRequired()
                .HasMaxLength(256) // Adjust length based on expected email size
                .HasAnnotation("Email", "Email must be in correct format");

            builder.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(60);

            builder.Property(s => s.MobileNumber)
                .IsRequired()
                .HasMaxLength(20); // Adjust length based on expected phone number format

            builder.Property(s => s.PersonalId)
                .IsRequired()
                .HasMaxLength(11);

            builder.Property(s => s.Sex)
                .IsRequired();

            // Configuring the Address relationship
            builder.HasOne(s => s.Address)
                .WithOne(a => a.Client)
                .HasForeignKey<Address>(a => a.ClientId);

            // Configuring the Accounts relationship
            builder.HasMany(s => s.Accounts)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId);

            // Apply additional constraints if necessary
            builder.HasIndex(s => s.Email).IsUnique(); // Ensure Email is unique
        }
    }
}