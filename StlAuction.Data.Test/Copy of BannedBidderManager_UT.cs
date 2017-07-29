using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class BidderPhotoIdAddressManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_BidderPhotoIdAddressManager()
        {
            // Setup

            var bidderPhotoIdAddressManager = new BidderPhotoIdAddressManager();
            bidderPhotoIdAddressManager.RemoveAllBidderPhotoIdAddresss();

            var originalBidderPhotoIdAddressCount = bidderPhotoIdAddressManager.GetNumberOfBidderPhotoIdAddresses();

            var testBidderPhotoIdAddress1 = new BidderPhotoIdAddress
            {
                CityAddress = "St Louis",
                StateAddress = "MO",
                ZipAddress = "63104",
                StreetAddress = "124 Test"
            };

            var id = bidderPhotoIdAddressManager.Save(testBidderPhotoIdAddress1);

            Assert.AreEqual(id, testBidderPhotoIdAddress1.Id);
            Assert.IsTrue(id > 0);

            var newBidderPhotoIdAddressCount = bidderPhotoIdAddressManager.GetNumberOfBidderPhotoIdAddresses();

            Assert.AreEqual(originalBidderPhotoIdAddressCount + 1, newBidderPhotoIdAddressCount);

            testBidderPhotoIdAddress1.StreetAddress = "42 Test";

            bidderPhotoIdAddressManager.Update(testBidderPhotoIdAddress1);

            var testBannedBidder2 = bidderPhotoIdAddressManager.GetById(testBidderPhotoIdAddress1.Id);

            Assert.AreEqual(testBannedBidder2.StreetAddress, "42 Test");

            bidderPhotoIdAddressManager.RemoveAllBidderPhotoIdAddresss();

            newBidderPhotoIdAddressCount = bidderPhotoIdAddressManager.GetNumberOfBidderPhotoIdAddresses();

            Assert.AreEqual(originalBidderPhotoIdAddressCount, newBidderPhotoIdAddressCount);
        }

        [TestMethod]
        public void TestGetAll_BidderPhotoIdAddressManager()
        {
            var bidderPhotoIdAddressManager = new BidderPhotoIdAddressManager();

            bidderPhotoIdAddressManager.RemoveAllBidderPhotoIdAddresss();

            var testBidderPhotoIdAddress1 = new BidderPhotoIdAddress
            {
               CityAddress = "St Louis",
               StateAddress = "MO",
               ZipAddress = "63104",
               StreetAddress = "124 Test"
            };

            var testBidderPhotoIdAddress2 = new BidderPhotoIdAddress
            {
                CityAddress = "St Louis",
                StateAddress = "MO",
                ZipAddress = "63104",
                StreetAddress = "124 Test"
            };

            var testBidderPhotoIdAddress3 = new BidderPhotoIdAddress
            {
                CityAddress = "St Louis",
                StateAddress = "MO",
                ZipAddress = "63104",
                StreetAddress = "124 Test"
            };

            bidderPhotoIdAddressManager.Save(testBidderPhotoIdAddress1);
            bidderPhotoIdAddressManager.Save(testBidderPhotoIdAddress2);
            bidderPhotoIdAddressManager.Save(testBidderPhotoIdAddress3);

            var bidderPhotoIdAddresses = bidderPhotoIdAddressManager.GetAllBidderPhotoIdAddresss();

            Assert.AreEqual(3, bidderPhotoIdAddresses.Count);

            bidderPhotoIdAddressManager.RemoveAllBidderPhotoIdAddresss();
        }


    }
}
