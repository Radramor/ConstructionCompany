using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using СonstructionСompany.Model;
using System.Reflection.Emit;

namespace СonstructionСompany.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> user)
        {
            user.HasKey(u => u.Id);
            user.Property(u => u.Login).IsRequired().HasMaxLength(100);
            user.Property(u => u.Password).IsRequired();
            user.Property(u => u.Role).IsRequired().HasMaxLength(50);
        }
    }
}
