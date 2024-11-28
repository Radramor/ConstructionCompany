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
    internal class EstimateConfiguration : IEntityTypeConfiguration<Estimate>
    {
        public void Configure(EntityTypeBuilder<Estimate> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(e => e.Object).WithMany(o => o.Estimates).HasForeignKey(e => e.ObjectId);
        }
    }
}
