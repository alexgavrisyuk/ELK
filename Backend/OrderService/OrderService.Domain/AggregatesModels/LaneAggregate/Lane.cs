using System;
using System.Collections.Generic;
using System.Linq;
using ContractManager.Domain.AggregatesModel.LaneAggregate;
using ContractManager.Domain.AggregatesModel.RegionAggregate;
using OrderService.Domain.AggregatesModels.RegionAggregate;

namespace OrderService.Domain.AggregatesModels.LaneAggregate
{
    public class Lane
    {
        public int Id { get; set; }

        public LanePricingType PricingType { get; private set; }

        public LaneType LaneType { get; private set; }

        public MatrixLaneType MatrixLaneType { get; private set; }

        public RateType RateType { get; private set; }

        public decimal Rate { get; private set; }

        public DateTime LastModified { get; private set; }

        public int SourceRegionId { get; private set; }
        public Region SourceRegion { get; private set; }

        public int DestinationRegionId { get; private set; }
        public Region DestinationRegion { get; private set; }

        private readonly List<TariffXrefLane> _tariffs = new List<TariffXrefLane>();
        public IReadOnlyCollection<TariffXrefLane> Tariffs => _tariffs;

        public Lane(
            LanePricingType pricingType, LaneType laneType, MatrixLaneType matrixLaneType, RateType rateType,
            decimal rate, int sourceRegionId, int destinationRegionId)
        {
            if (sourceRegionId == destinationRegionId)
            {
                throw new Exception(
                    $"Source with id {sourceRegionId} and destination {destinationRegionId} Regions should different");
            }

            PricingType = pricingType;
            LaneType = laneType;
            MatrixLaneType = matrixLaneType;
            RateType = rateType;
            Rate = rate;
            SourceRegionId = sourceRegionId;
            DestinationRegionId = destinationRegionId;

            LastModified = DateTime.UtcNow;
        }

        public void Update(
            LanePricingType pricingType, LaneType laneType, MatrixLaneType matrixLaneType, RateType rateType,
            decimal rate, int sourceRegionId, int destinationRegionId)
        {
            if (sourceRegionId == destinationRegionId)
            {
                throw new Exception(
                    $"Source with id {sourceRegionId} and destination {destinationRegionId} Regions should different");
            }

            PricingType = pricingType;
            LaneType = laneType;
            MatrixLaneType = matrixLaneType;
            RateType = rateType;
            Rate = rate;
            SourceRegionId = sourceRegionId;
            DestinationRegionId = destinationRegionId;

            LastModified = DateTime.UtcNow;
        }

        public void ChangeWay(int newSourceRegionId, int newDestinationRegionId)
        {
            SourceRegionId = newSourceRegionId;
            DestinationRegionId = newDestinationRegionId;

            LastModified = DateTime.UtcNow;
        }

        public void ChangeSource(int newSourceRegionId)
        {
            SourceRegionId = newSourceRegionId;

            LastModified = DateTime.UtcNow;
        }

        public void ChangeDestination(int newDestinationRegionId)
        {
            DestinationRegionId = newDestinationRegionId;

            LastModified = DateTime.UtcNow;
        }

        public void AddTariff(int tariffId)
        {
            var item = new TariffXrefLane(tariffId, Id);
            _tariffs.Add(item);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteTariff(int tariffId)
        {
            var item = _tariffs.FirstOrDefault(l => l.TariffId == tariffId);
            if (item == null)
            {
                throw new Exception($"Lane with id {Id} does not contains Tariff with id {tariffId}");
            }

            _tariffs.Remove(item);

            LastModified = DateTime.UtcNow;
        }
    }
}