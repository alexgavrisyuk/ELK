using System;
using ContractManager.Domain.AggregatesModel.TariffAggregate;
using OrderService.Domain.AggregatesModels.TariffAggregate;

namespace OrderService.Domain.AggregatesModels.TariffExtra
{
    public class TariffExtra 
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public TariffExtraType Type { get; private set; }

        public ExtraApplicationType ApplicationType { get; private set; }

        public decimal Rate { get; private set; }

        public FuelRateType FuelRateType { get; private set; }

        public AccessorialRateType AccessorialRateType { get; set; }

        public DateTime LastModified { get; private set; }

        public int TariffId { get; private set; }
        public Tariff Tariff { get; private set; }

        public TariffExtra(
            string name, string description, TariffExtraType type, ExtraApplicationType applicationType, decimal rate,
            FuelRateType fuelRateType, AccessorialRateType accessorialRateType, int tariffId)
        {
            Name = name;
            Description = description;
            Type = type;
            ApplicationType = applicationType;
            Rate = rate;
            FuelRateType = fuelRateType;
            AccessorialRateType = accessorialRateType;
            TariffId = tariffId;

            LastModified = DateTime.UtcNow;
        }

        public void Update(
            string name, string description, TariffExtraType type, ExtraApplicationType applicationType, decimal rate,
            FuelRateType fuelRateType, AccessorialRateType accessorialRateType, int? tariffId)
        {
            if (tariffId.HasValue)
            {
                TariffId = tariffId.Value;
            }

            Name = name;
            Description = description;
            Type = type;
            ApplicationType = applicationType;
            Rate = rate;
            FuelRateType = fuelRateType;
            AccessorialRateType = accessorialRateType;

            LastModified = DateTime.UtcNow;
        }
    }
}