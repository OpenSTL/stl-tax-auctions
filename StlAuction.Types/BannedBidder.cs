using System;

namespace StlAuction.Types
{
    public class BannedBidder : IHasLongId
    {
        public long Id { get; set; }

        public DateTime Date { get; set; }
        public String SuitNumber { get; set; }
        public String CompanyAndIndividualName { get; set; }
        public String Address { get; set; }
        public String PhoneNumbers { get; set; }
        public String Reason { get; set; }

    }
}
