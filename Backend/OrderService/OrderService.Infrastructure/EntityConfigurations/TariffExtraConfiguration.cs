using ContractManager.Domain.AggregatesModel.TariffAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.TariffExtra;

namespace ContractManager.Infrastructure.EntityConfigurations
{
    public class TariffExtraConfiguration : IEntityTypeConfiguration<TariffExtra>
    {
        public void Configure(EntityTypeBuilder<TariffExtra> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired();

            builder.Property(c => c.Description).IsRequired();

            builder.HasOne(c => c.Tariff).WithMany(t => t.TariffExtras).HasForeignKey(c => c.TariffId);
        }
    }
}
