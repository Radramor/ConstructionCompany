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
    internal class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.NameOfOrganization).HasMaxLength(150).IsRequired();
            builder.Property(s => s.FirstNameOfSupervisor).HasMaxLength(50);
            builder.Property(s => s.SecondNameOfSupervisor).HasMaxLength(50);
            builder.Property(s => s.PatronymicOfSupervisor).HasMaxLength(50);
            builder.Property(s => s.Phone).HasMaxLength(20);
            builder.HasOne(s => s.Bank).WithMany(b => b.Suppliers).HasForeignKey(s => s.BankId);
        }
    }
}
