namespace OrderService.Domain.AggregatesModels.RegionAggregate
{
    public class RegionXrefRegion
    {
        public int Id { get; set; }

        public int ParentRegionId { get; private set; }
        public Region ParentRegion { get; private set; }
        
        public int ChildRegionId { get; private set; }
        public Region ChildRegion { get; private set; }

        private RegionXrefRegion()
        {
            // Need for EF
        }

        public RegionXrefRegion(int parentRegionId, int childRegionId)
        {
            ParentRegionId = parentRegionId;
            ChildRegionId = childRegionId;
        }
    }
}
