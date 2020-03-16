using ContractManager.Domain.AggregatesModel.ContractAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.AggregatesModels.ContractAggregate;

namespace OrderService.Infrastructure.EntityConfigurations
{
    public class ContractConfiguration : IEntityTypeConfiguration<Contract>
    {
        public void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name).IsRequired().HasMaxLength(Constraints.Contract.NameMaxLength);

            builder.Property(c => c.Description).IsRequired().HasMaxLength(Constraints.Contract.DescriptionMaxLength);

            builder.HasMany(c => c.Tariffs);
        }
    }
}
