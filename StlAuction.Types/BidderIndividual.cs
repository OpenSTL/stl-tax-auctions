namespace StlAuction.Types
{
    public class BidderIndividual : IHasLongId
    {
        public long Id { get; set; }
        
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string StateAddress { get; set; }
        public string ZipAddress { get; set; }
        public string HomePhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
    }
}
