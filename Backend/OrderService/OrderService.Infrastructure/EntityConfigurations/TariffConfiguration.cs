using ContractManager.Domain.AggregatesModel.TariffAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.TariffAggregate;

namespace ContractManager.Infrastructure.EntityConfigurations
{
    public class TariffConfiguration : IEntityTypeConfiguration<Tariff>
    {
        public void Configure(EntityTypeBuilder<Tariff> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Description).IsRequired();

            builder.HasMany(c => c.Contracts);

            builder.HasMany(c => c.Lanes);
       
            builder.HasMany(c => c.Conditionals);
      
            builder.HasMany(c => c.Discounts);
            
            builder.HasMany(c => c.TariffExtras);
        }
    }
}
