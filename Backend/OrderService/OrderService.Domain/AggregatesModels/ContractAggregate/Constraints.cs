namespace ContractManager.Domain.AggregatesModel.ContractAggregate
{
    public static class Constraints
    {
        public static class Contract
        {
            public const int NameMaxLength = 100;

            public const int DescriptionMaxLength = 250;
        }
    }
}