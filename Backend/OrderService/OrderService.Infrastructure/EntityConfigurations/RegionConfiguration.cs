using ContractManager.Domain.AggregatesModel.RegionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.RegionAggregate;

namespace ContractManager.Infrastructure.EntityConfigurations
{
    public class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Description).IsRequired();
        }
    }
}
