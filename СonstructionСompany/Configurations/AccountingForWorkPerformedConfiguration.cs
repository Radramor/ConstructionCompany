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
    internal class AccountingForWorkPerformedConfiguration : IEntityTypeConfiguration<AccountingForWorkPerformed>
    {
        public void Configure(EntityTypeBuilder<AccountingForWorkPerformed> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Date).IsRequired();
            builder.HasOne(a => a.Brigade).WithMany(b => b.accountingForWorkPerformeds).HasForeignKey(a => a.BrigadeId);
            builder.HasOne(a => a.Object).WithMany(o => o.AccountingForWorkPerformeds).HasForeignKey(a => a.ObjectId);
        }
    }
}
