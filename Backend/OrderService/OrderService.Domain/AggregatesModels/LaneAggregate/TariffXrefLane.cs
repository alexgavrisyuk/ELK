using ContractManager.Domain.AggregatesModel.LaneAggregate;
using ContractManager.Domain.AggregatesModel.TariffAggregate;
using OrderService.Domain.AggregatesModels.TariffAggregate;

namespace OrderService.Domain.AggregatesModels.LaneAggregate
{
    public class TariffXrefLane 
    {
        public int Id { get; set; }

        public int TariffId { get; private set; }
        public Tariff Tariff { get; private set; }

        public int LaneId { get; private set; }
        public Lane Lane { get; private set; }

        private TariffXrefLane()
        {
            // Need for EF
        }

        public TariffXrefLane(int tariffId, int laneId)
        {
            TariffId = tariffId;
            LaneId = laneId;
        }
    }
}
