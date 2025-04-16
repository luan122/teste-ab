using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Entities.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class ProductRateConfiguration : IEntityTypeConfiguration<ProductRate>
    {
        public void Configure(EntityTypeBuilder<ProductRate> builder)
        {
            builder.ToTable("Products_Rates");
            builder.HasKey(pr => pr.Id);
            builder.Property(pr => pr.Id).HasColumnType("uuid").HasDefaultValueSql("gen_random_uuid()");
            builder.Property(p => p.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(pr => pr.Rate).IsRequired();
            builder.HasOne(pr => pr.Product)
                .WithOne(p => p.Rating)
                .HasForeignKey<ProductRate>(pr => pr.ProductId)
                .OnDelete(DeleteBehavior.NoAction)
                .IsRequired();

            builder.HasIndex(pr => pr.ProductId, "IX_ProductsRates_Product_Id_Ascending");
            builder.HasIndex(pr => pr.Rate, "IX_ProductsRates_Rate_Ascending");
            builder.HasIndex(pr => pr.Count, "IX_ProductsRates_Count_Ascending");
        }
    }
}
