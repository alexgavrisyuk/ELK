namespace ContractManager.Domain.AggregatesModel.LaneAggregate
{
    public enum LaneType
    {
        PointToPoint = 0,
        ZipToZip = 1,
        PointToRegion = 2,
        RegionToPoint = 3,
        RegionToRegion = 4
    }
}