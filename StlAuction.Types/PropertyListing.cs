namespace StlAuction.Types
{
    public class PropertyListing : IHasLongId
    {
        public long Id { get; set; }

        public string LandTaxNumber { get; set; }
        public string Owner { get; set; }
        public string Address { get; set; }
        public decimal Total { get; set; }

    }
}
