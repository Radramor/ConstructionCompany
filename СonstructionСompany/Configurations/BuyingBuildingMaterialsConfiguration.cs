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
    internal class BuyingBuildingMaterialsConfiguration : IEntityTypeConfiguration<BuyingBuildingMaterials>
    {
        public void Configure(EntityTypeBuilder<BuyingBuildingMaterials> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Date).IsRequired();
            builder.Property(b => b.TotalPrice).HasColumnType("decimal(18,2)").IsRequired();
            builder.HasOne(b => b.Supplier).WithMany(s => s.BuyingBuildingMaterials).HasForeignKey(b => b.SupplierId);
            builder.HasOne(b => b.Warehouse).WithMany(w => w.BuyingBuildingMaterials).HasForeignKey(b => b.WarehouseId);
        }
    }
}
