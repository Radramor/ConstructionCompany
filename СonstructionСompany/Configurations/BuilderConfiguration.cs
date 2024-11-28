using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;

namespace СonstructionСompany.Configurations
{
    internal class BuilderConfiguration : IEntityTypeConfiguration<Builder>
    {
        public void Configure(EntityTypeBuilder<Builder> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.FirstName).HasMaxLength(50).IsRequired();
            builder.Property(b => b.SecondName).HasMaxLength(50);
            builder.Property(b => b.Patronymic).HasMaxLength(50);
            builder.Property(b => b.DateOfBirth).IsRequired();
            builder.Property(b => b.ResidentialAddress).HasMaxLength(200);
            builder.Property(b => b.LengthOfService).IsRequired();
            builder.Property(b => b.Specialties).HasMaxLength(200);
            builder.HasOne(b => b.Brigade).WithMany(b => b.Builders).HasForeignKey(b => b.BrigadeId);
        }
    }
}
