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
    internal class BrigadeConfiguration : IEntityTypeConfiguration<Brigade>
    {
        public void Configure(EntityTypeBuilder<Brigade> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100);
            builder.HasMany(b => b.Builders).WithOne(b => b.Brigade).HasForeignKey(b => b.BrigadeId);
            builder.HasMany(b => b.accountingForWorkPerformeds).WithOne(a => a.Brigade).HasForeignKey(a => a.BrigadeId);
        }
    }
}
