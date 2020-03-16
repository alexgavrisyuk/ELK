using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.LaneAggregate;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class LaneConfiguration : IEntityTypeConfiguration<Lane>
    {
        public void Configure(EntityTypeBuilder<Lane> builder)
        {
            builder.HasKey(c => c.Id);

            builder.HasOne(c => c.SourceRegion);
            
            builder.HasOne(c => c.DestinationRegion);
        
            builder.HasMany(c => c.Tariffs);

        }
    }
}
