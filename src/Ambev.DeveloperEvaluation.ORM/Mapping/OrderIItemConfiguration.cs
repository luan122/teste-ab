using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class OrderIItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.ToTable("OrderItems");

            builder.Property(o => o.Quantity)
                .IsRequired();

            builder.HasOne(o => o.Product)
                .WithMany()
                .HasForeignKey(o => o.ProductId)
                .IsRequired();

            builder.HasIndex(o => o.OrderId, "IX_OrderItems_OrderId_Ascending");
            builder.HasIndex(o => o.ProductId, "IX_OrderItems_ProductId_Ascending");
        }
    }
}
