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
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(o => o.OrderNumber).UseSerialColumn();

            builder.Property(o => o.BranchId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(o => o.CustomerId)
                .HasColumnType("uuid")
                .IsRequired();

            builder.Property(o => o.IsCanceled)
                .HasDefaultValue(false);

            builder.HasOne(o => o.Customer)
                .WithMany(o => o.Orders)
                .HasForeignKey(o => o.CustomerId)
                .IsRequired();

            builder.HasMany(o => o.Items)
                .WithOne(o => o.Order)
                .HasForeignKey(o => o.OrderId)
                .IsRequired();

            builder.HasIndex(builder => builder.CustomerId, "IX_Orders_CustomerId_Ascending");
            builder.HasIndex(builder => builder.BranchId, "IX_Orders_BranchId_Ascending");

        }
    }
}
