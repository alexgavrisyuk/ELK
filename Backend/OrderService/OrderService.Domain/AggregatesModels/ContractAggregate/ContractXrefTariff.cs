using ContractManager.Domain.AggregatesModel.TariffAggregate;
using OrderService.Domain.AggregatesModels.TariffAggregate;

namespace OrderService.Domain.AggregatesModels.ContractAggregate
{
    public class ContractXrefTariff
    {
        public int Id { get; set; }

        public int ContractId { get; private set; }
        public Contract Contract { get; private set; }

        public int TariffId { get; private set; }
        public Tariff Tariff { get; private set; }

        public ContractXrefTariff(int tariffId, int contractId)
        {
            TariffId = tariffId;
            ContractId = contractId;
        }
    }
}