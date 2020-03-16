using System;
using ContractManager.Domain.AggregatesModel.RegionAggregate;

namespace OrderService.Domain.AggregatesModels.RegionAggregate
{
    public class Region 
    {
        public int Id { get; set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public RegionType Type { get; private set; }

        public DateTime LastModified { get; private set; }

        private Region()
        {
        }

        public Region(
            string name, string description, RegionType type)
        {
            Name = name;
            Description = description;
            Type = type;

            LastModified = DateTime.UtcNow;
        }

        public void Update(
            string name, string description, RegionType type)
        {
            Name = name;
            Description = description;
            Type = type;

            LastModified = DateTime.UtcNow;
        }
    }
}
