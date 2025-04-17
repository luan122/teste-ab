using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder.ToTable("Branches");

            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(b => b.Address)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(b => b.ZipCode)
                .IsRequired()
                .HasMaxLength(12);

            builder.Property(b => b.City)
                .IsRequired()
                .HasMaxLength(50);
            builder.Property(b => b.State)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(b => b.Name, "IX_Branches_Name_Ascending");
            builder.HasIndex(b => b.Name, "IX_Branches_Name_Descending")
                .IsDescending();
        }
    }
}
