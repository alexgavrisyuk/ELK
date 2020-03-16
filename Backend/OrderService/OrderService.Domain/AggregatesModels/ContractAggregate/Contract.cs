using System;
using System.Collections.Generic;
using System.Linq;
using ContractManager.Domain.AggregatesModel.ContractAggregate;

namespace OrderService.Domain.AggregatesModels.ContractAggregate
{
    public class Contract
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string CustomerName { get; private set; }

        public bool IsActive { get; private set; }

        public int Priority { get; private set; }

        public string Description { get; private set; }

        public DateTime LastModified { get; private set; }

        public DateTime ExpirationDate { get; private set; }

        public OrderCreationType OrderCreationType { get; private set; }

        private readonly List<ContractXrefTariff> _tariffs = new List<ContractXrefTariff>();
        public IReadOnlyCollection<ContractXrefTariff> Tariffs => _tariffs;

        private Contract()
        {
            // Need for EF
        }

        public Contract(
            string name, string customerName, bool isActive, int priority, string description,
            DateTime expirationDate, OrderCreationType creationType)
        {
            Name = name;
            CustomerName = customerName;
            IsActive = isActive;
            Priority = priority;
            Description = description;
            ExpirationDate = expirationDate;
            OrderCreationType = creationType;

            LastModified = DateTime.UtcNow;
        }

        public void Update(
            string name, string customerName, bool isActive, int priority, string description,
            DateTime expirationDate, OrderCreationType creationType)
        {
            Name = name;
            CustomerName = customerName;
            IsActive = isActive;
            Priority = priority;
            Description = description;
            ExpirationDate = expirationDate;
            OrderCreationType = creationType;

            LastModified = DateTime.UtcNow;
        }

        public void AddTariff(int tariffId)
        {
            var item = new ContractXrefTariff(tariffId, Id);
            _tariffs.Add(item);

            LastModified = DateTime.UtcNow;
        }

        public void DeleteTariff(int tariffId)
        {
            var item = _tariffs.FirstOrDefault(c => c.TariffId == tariffId);
            if (item == null)
            {
                throw new Exception($"Contract with id {Id} does not contains Tariff with id {tariffId}");
            }

            _tariffs.Remove(item);

            LastModified = DateTime.UtcNow;
        }
    }
}