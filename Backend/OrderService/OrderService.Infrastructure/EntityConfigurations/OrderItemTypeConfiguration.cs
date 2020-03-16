using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.OrderAggregate;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class OrderItemTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasOne(b => b.Dish);
        }
    }
}