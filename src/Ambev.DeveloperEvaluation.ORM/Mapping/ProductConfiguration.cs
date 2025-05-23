﻿using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.Property(p => p.Description).IsRequired();
        builder.Property(p => p.Price).IsRequired();
        builder.Property(p => p.Title).IsRequired();
        builder.Property(p => p.IsActive).HasDefaultValue(true);

        builder.HasIndex(o => o.Price, "IX_Products_Price_Ascending");

        builder.HasIndex(o => o.Price, "IX_Products_Price_Descending")
            .IsDescending();


        builder.HasIndex(o => o.Title, "IX_Products_Title_Ascending")
            .IsUnique();

        builder.HasIndex(o => o.Title, "IX_Products_Title_Descending")
            .IsUnique()
            .IsDescending();

        builder.HasOne(p => p.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.CategoryId)
             .HasPrincipalKey(p => p.Id)
             .OnDelete(DeleteBehavior.NoAction)
             .IsRequired();


    }
}
