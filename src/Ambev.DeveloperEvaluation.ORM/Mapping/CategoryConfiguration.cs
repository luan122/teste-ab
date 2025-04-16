using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace Ambev.DeveloperEvaluation.ORM.Mapping;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("Categories");

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .HasColumnType("uuid")
            .HasDefaultValueSql("gen_random_uuid()");

        builder.Property(o => o.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(o => o.Title, "IX_Categories_Title_Ascending")
            .IsUnique();

        builder.HasIndex(o => o.Title, "IX_Categories_Title_Descending")
            .IsUnique()
            .IsDescending();

        builder.Property(o => o.CreatedAt)
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(o => o.IsDeleted)
            .HasDefaultValue(false);

        builder.HasMany(o => o.Products)
            .WithOne(o => o.Category)
            .HasForeignKey(o => o.CategoryId);

    }
}
