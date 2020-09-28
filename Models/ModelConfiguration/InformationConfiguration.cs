using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Models.ModelConfiguration
{
    public class InformationConfiguration : IEntityTypeConfiguration<Information>
    {
        public void Configure(EntityTypeBuilder<Information> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired();
            builder.Property(e => e.Content).IsRequired();
            builder.HasOne<ApplicationUser>(a => a.ApplicationUser)
                .WithMany(b => b.Informations)
                .HasForeignKey(b => b.UserId);
        }
    }
}
