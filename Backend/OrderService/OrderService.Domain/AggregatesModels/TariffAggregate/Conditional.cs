using ContractManager.Domain.AggregatesModel.TariffAggregate;

namespace OrderService.Domain.AggregatesModels.TariffAggregate
{
    public class Conditional 
    {
        public int Id { get; set; }

        public ConditionalPackagingType PackagingType { get; private set; }

        public int Amount { get; private set; }

        public int TariffId { get; private set; }
        public Tariff Tariff { get; private set; }

        private Conditional()
        {
        }

        public Conditional(int id)
        {
            Id = id;
        }
    }
}
