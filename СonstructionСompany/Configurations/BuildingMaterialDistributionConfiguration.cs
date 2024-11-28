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
    internal class BuildingMaterialDistributionConfiguration : IEntityTypeConfiguration<BuildingMaterialDistribution>
    {
        public void Configure(EntityTypeBuilder<BuildingMaterialDistribution> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Date).IsRequired();
            builder.HasOne(b => b.Object).WithMany(o => o.BuildingMaterialDistributions).HasForeignKey(b => b.ObjectId);
            builder.HasOne(b => b.Warehouse).WithMany(w => w.MaterialDistributions).HasForeignKey(b => b.WarehouseId);
        }
    }
}
