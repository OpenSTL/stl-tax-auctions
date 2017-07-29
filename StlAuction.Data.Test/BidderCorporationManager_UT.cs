using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class BidderCorporationManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_BidderCorporationManager()
        {
            var bidderCorporationManager = new BidderCorporationManager();

            bidderCorporationManager.RemoveAllBidderCorporations();

            var originalBidderCoporationCount = bidderCorporationManager.GetNumberOfBidderCorporations();

            var testBidderCorporation = new BidderCorporation
            {
              CityAddress = "St Louis",
              Name = "xyz co.",
              StateAddress = "MO",
              StreetAddress = "123 Test",
              ZipAddress = "63104"
            };

            var id = bidderCorporationManager.Save(testBidderCorporation);

            Assert.AreEqual(id, testBidderCorporation.Id);
            Assert.IsTrue(id > 0);

            var newBidderCoporationCount = bidderCorporationManager.GetNumberOfBidderCorporations();

            Assert.AreEqual(originalBidderCoporationCount + 1, newBidderCoporationCount);
            
            testBidderCorporation.ZipAddress = "63105";

            bidderCorporationManager.Update(testBidderCorporation);

            var testBannedBidder2 = bidderCorporationManager.GetById(testBidderCorporation.Id);

            Assert.AreEqual(testBannedBidder2.ZipAddress, "63105");

            bidderCorporationManager.RemoveBidderCorporation(testBidderCorporation);

            newBidderCoporationCount = bidderCorporationManager.GetNumberOfBidderCorporations();

            Assert.AreEqual(originalBidderCoporationCount, newBidderCoporationCount);
        }
        
        [TestMethod]
        public void TestGetAll_BidderCorporationManager()
        {
            var bidderCorporationManager = new BidderCorporationManager();

            bidderCorporationManager.RemoveAllBidderCorporations();

            var originalBidderCoporationCount = bidderCorporationManager.GetNumberOfBidderCorporations();
            Assert.AreEqual(0, originalBidderCoporationCount);

            var testBidderCorporation1 = new BidderCorporation
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "123 Test",
                ZipAddress = "63104"
            };

            var testBidderCorporation2 = new BidderCorporation
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "124 Test",
                ZipAddress = "63104"
            };

            var testBidderCorporation3 = new BidderCorporation
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "125 Test",
                ZipAddress = "63104"
            };

            bidderCorporationManager.Save(testBidderCorporation1);
            bidderCorporationManager.Save(testBidderCorporation2);
            bidderCorporationManager.Save(testBidderCorporation3);

            var bidderCorporations = bidderCorporationManager.GetAllBidderCorporations();

            Assert.AreEqual(3, bidderCorporations.Count);

            Assert.IsTrue(bidderCorporations.Count(b => b.StreetAddress == "125 Test") == 1);

            bidderCorporationManager.RemoveAllBidderCorporations();
        }


    }
}
