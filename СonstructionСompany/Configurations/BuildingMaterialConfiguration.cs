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
    internal class BuildingMaterialConfiguration : IEntityTypeConfiguration<BuildingMaterial>
    {
        public void Configure(EntityTypeBuilder<BuildingMaterial> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name).HasMaxLength(100).IsRequired();
            builder.Property(b => b.Unit).HasMaxLength(20).IsRequired();
            builder.Property(b => b.Price).IsRequired().HasColumnType("decimal(18,2)");
            builder.HasOne(b => b.Supplier).WithMany(s => s.BuildingMaterials).HasForeignKey(b => b.SupplierId);
        }
    }
}
