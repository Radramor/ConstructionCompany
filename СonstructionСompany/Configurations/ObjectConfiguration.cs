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
    internal class ObjectConfiguration : IEntityTypeConfiguration<Model.Object>
    {
        public void Configure(EntityTypeBuilder<Model.Object> builder)
        {
            builder.HasKey(o => o.Id);
            builder.Property(o => o.Name).HasMaxLength(100).IsRequired();
            builder.Property(o => o.Address).HasMaxLength(200).IsRequired();
        }
    }
}
