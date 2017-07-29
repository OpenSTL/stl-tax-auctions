namespace StlAuction.Types
{
    public class PropertyBid : IHasLongId
    {
        public long Id { get; set; }

        public string LandTaxSuitNumber { get; set; }
        public decimal Amount { get; set; }
        public long BidderNumber { get; set; }
    }
}
