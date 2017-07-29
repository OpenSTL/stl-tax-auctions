using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class WinningBidManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_WinningBidManager()
        {
            var winningBidManager = new WinningBidManager();
            winningBidManager.RemoveAllWinningBids();
            var originalBannedBidderCount = winningBidManager.GetNumberOfWinningBids();
            var testWinningBid = new PropertyBid
            {
               BidderNumber = 26534,
               Amount = (decimal) 1729.21,
               LandTaxSuitNumber = "188-021-3"
            };
            var id = winningBidManager.Save(testWinningBid);

            Assert.AreEqual(id, testWinningBid.Id);
            Assert.IsTrue(id > 0);

            var newWinningBidCount = winningBidManager.GetNumberOfWinningBids();

            Assert.AreEqual(originalBannedBidderCount + 1, newWinningBidCount);
            
            testWinningBid.Amount = 42;
            winningBidManager.Update(testWinningBid);
            var testWinningBid2 = winningBidManager.GetById(testWinningBid.Id);

            Assert.AreEqual(testWinningBid2.Amount, 42);

            winningBidManager.RemoveWinningBid(testWinningBid);
            newWinningBidCount = winningBidManager.GetNumberOfWinningBids();

            Assert.AreEqual(originalBannedBidderCount, newWinningBidCount);
        }


        [TestMethod]
        public void TestGetAll_WinningBidManager()
        {
            var winningBidManager = new WinningBidManager();

            winningBidManager.RemoveAllWinningBids();

            var testWinningBid1 = new PropertyBid
            {
                BidderNumber = 26534,
                Amount = (decimal)1729.21,
                LandTaxSuitNumber = "188-021-3"
            };

            var testWinningBid2 = new PropertyBid
            {
                BidderNumber = 26535,
                Amount = 43,
                LandTaxSuitNumber = "188-021-4"
            };

            var testWinningBid3 = new PropertyBid
            {
                BidderNumber = 26536,
                Amount = 834,
                LandTaxSuitNumber = "188-021-7"
            };

            winningBidManager.Save(testWinningBid1);
            winningBidManager.Save(testWinningBid2);
            winningBidManager.Save(testWinningBid3);

            var winningBids = winningBidManager.GetAllWinningBids();

            Assert.AreEqual(3, winningBids.Count);

            winningBidManager.RemoveAllWinningBids();
        }
    }
}
