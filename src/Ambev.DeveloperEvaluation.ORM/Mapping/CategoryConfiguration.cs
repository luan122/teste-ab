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

        builder.Property(o => o.Title)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(o => o.Title, "IX_Categories_Title_Ascending")
            .IsUnique();

        builder.HasIndex(o => o.Title, "IX_Categories_Title_Descending")
            .IsUnique()
            .IsDescending();

    }
}
