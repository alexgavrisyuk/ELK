using System;
using ContractManager.Domain.AggregatesModel.TariffAggregate;

namespace OrderService.Domain.AggregatesModels.TariffAggregate
{
    public class Discount 
    {
        public int Id { get; set; }

        public DiscountType Type { get; private set; }

        public int Priority { get; private set; }

        public decimal DiscountValue { get; private set; }

        public decimal MinValue { get; private set; }

        public decimal MaxValue { get; private set; }

        public DateTime LastModified { get; private set; }

        public int TariffId { get; private set; }
        public Tariff Tariff { get; private set; }

        private Discount()
        {
        }

        public Discount(int id)
        {
            Id = id;
        }

        public Discount(
            DiscountType type, int priority, decimal discountValue, decimal minValue, decimal maxValue, int tariffId)
        {
            Type = type;
            Priority = priority;
            DiscountValue = discountValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }

        public void Update(
            DiscountType type, int priority, decimal discountValue, decimal minValue, decimal maxValue, int tariffId)
        {
            Type = type;
            Priority = priority;
            DiscountValue = discountValue;
            MinValue = minValue;
            MaxValue = maxValue;
        }
    }
}