

namespace StlAuction.Types
{
    public class BidderPhotoIdAddress : IHasLongId
    {
        public long Id { get; set; }

        public string StreetAddress { get; set; }
        public string CityAddress { get; set; }
        public string StateAddress { get; set; }
        public string ZipAddress { get; set; }
       
    }
}
