using System;
using System.Collections.Generic;
using System.Linq;
using ContractManager.Domain.AggregatesModel.TariffAggregate;
using OrderService.Domain.AggregatesModels.ContractAggregate;
using OrderService.Domain.AggregatesModels.LaneAggregate;

namespace OrderService.Domain.AggregatesModels.TariffAggregate
{
    public class Tariff
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public ApplicationType ApplicationType { get; private set; }

        public decimal MinimumRate { get; private set; }

        public UpchargeType UpchargeType { get; private set; }

        public decimal UpchargeValue { get; private set; }

        public PricingType PricingType { get; private set; }

        public DateTime LastModified { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        private readonly List<ContractXrefTariff> _contracts = new List<ContractXrefTariff>();
        public IReadOnlyCollection<ContractXrefTariff> Contracts => _contracts;

        private readonly List<TariffXrefLane> _lanes = new List<TariffXrefLane>();
        public IReadOnlyCollection<TariffXrefLane> Lanes => _lanes;
        
        private readonly List<Conditional> _conditionals = new List<Conditional>();
        public IReadOnlyCollection<Conditional> Conditionals => _conditionals;

        private readonly List<Discount> _discounts = new List<Discount>();
        public IReadOnlyCollection<Discount> Discounts => _discounts;
        
        private readonly List<TariffExtra.TariffExtra> _tariffExtras = new List<TariffExtra.TariffExtra>();
        public IReadOnlyCollection<TariffExtra.TariffExtra> TariffExtras => _tariffExtras;

        private Tariff()
        {
            // Need for EF
        }

        public Tariff(
            string name, string description, ApplicationType applicationType, decimal minimumRate, UpchargeType upchargeType,
            decimal upchargeValue, PricingType pricingType, DateTime expirationDate)
        {
            Name = name;
            Description = description;
            ApplicationType = applicationType;
            MinimumRate = minimumRate;
            UpchargeType = upchargeType;
            UpchargeValue = upchargeValue;
            PricingType = pricingType;
            ExpirationDate = expirationDate;

            LastModified = DateTime.UtcNow;
        }

        public void Update(
            string name, string description, ApplicationType applicationType, decimal minimumRate, UpchargeType upchargeType,
            decimal upchargeValue, PricingType pricingType, DateTime expirationDate)
        {
            Name = name;
            Description = description;
            ApplicationType = applicationType;
            MinimumRate = minimumRate;
            UpchargeType = upchargeType;
            UpchargeValue = upchargeValue;
            PricingType = pricingType;
            ExpirationDate = expirationDate;

            LastModified = DateTime.UtcNow;
        }

        public void AddContract(int contractId)
        {
            var item = new ContractXrefTariff(contractId, Id);
            _contracts.Add(item);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteContract(int contractId)
        {
            var item = _contracts.FirstOrDefault(c => c.ContractId == contractId);
            if (item == null)
            {
                throw new Exception($"Tariff with id {Id} does not contains Contract with id {contractId}");
            }

            _contracts.Remove(item);

            LastModified = DateTime.UtcNow;
        }

        public void AddLane(int laneId)
        {
            var item = new TariffXrefLane(Id, laneId);
            _lanes.Add(item);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteLane(int laneId)
        {
            var item = _lanes.FirstOrDefault(l => l.LaneId == laneId);
            if (item == null)
            {
                throw new Exception($"Tarrif with id {Id} does not contains Lane with id {laneId}");
            }

            _lanes.Remove(item);

            LastModified = DateTime.UtcNow;
        }

        public void AddConditional(int conditionalId)
        {
            var conditional = _conditionals.FirstOrDefault(c => c.Id == conditionalId);
            if (conditional != null)
            {
                return;
            }

            var newItem = new Conditional(conditionalId);
            _conditionals.Add(newItem);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteConditional(int conditionalId)
        {
            var item = _conditionals.FirstOrDefault(l => l.Id == conditionalId);
            if (item == null)
            {
                throw new Exception($"Tarrif with id {Id} does not contains Conditional with id {conditionalId}");
            }

            _conditionals.Remove(item);

            LastModified = DateTime.UtcNow;
        }

        public void AddDiscount(int discountId)
        {
            var discount = _discounts.FirstOrDefault(c => c.Id == discountId);
            if (discount != null)
            {
                return;
            }

            var newItem = new Discount(discountId);
            _discounts.Add(newItem);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteDiscount(int discountId)
        {
            var item = _discounts.FirstOrDefault(l => l.Id == discountId);
            if (item == null)
            {
                throw new Exception($"Tarrif with id {Id} does not contains Discount with id {discountId}");
            }

            _discounts.Remove(item);

            LastModified = DateTime.UtcNow;
        }
    }
}