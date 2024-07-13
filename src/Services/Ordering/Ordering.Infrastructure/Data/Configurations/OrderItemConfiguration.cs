﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configurations;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(oi => oi.Id);
        builder
            .Property(oi => oi.Id)
            .HasConversion(
                orderItemId => orderItemId.Value,
                dbId => OrderItemId.Of(dbId)
            );

        builder.Property(oi => oi.Quantity).IsRequired();

        builder.Property(oi => oi.Price).HasPrecision(18, 2).IsRequired();

        builder
            .HasOne<Product>()
            .WithMany()
            .HasForeignKey(oi => oi.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
