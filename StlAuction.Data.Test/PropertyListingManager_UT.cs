using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class PropertyListingManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_PropertyListingManager()
        {
            var propertyListingManager = new PropertyListingManager();
            propertyListingManager.RemoveAllPropertyListings();
            var originalNumberOfPropertyListings = propertyListingManager.GetNumberOfPropertyListings();
            var testPropertyListing = new PropertyListing
            {
               LandTaxNumber = "123-1234", 
               Address = "123 Test St", 
               Owner = "Plugh Properties", 
               Total = 234
            };
            var id = propertyListingManager.Save(testPropertyListing);

            Assert.AreEqual(id, testPropertyListing.Id);
            Assert.IsTrue(id > 0);

            var newWinningBidCount = propertyListingManager.GetNumberOfPropertyListings();

            Assert.AreEqual(originalNumberOfPropertyListings + 1, newWinningBidCount);
            
            testPropertyListing.Total = 42;
            propertyListingManager.Update(testPropertyListing);
            var testWinningBid2 = propertyListingManager.GetById(testPropertyListing.Id);

            Assert.AreEqual(testWinningBid2.Total, 42);

            propertyListingManager.RemovePropertyListing(testPropertyListing);
            newWinningBidCount = propertyListingManager.GetNumberOfPropertyListings();

            Assert.AreEqual(originalNumberOfPropertyListings, newWinningBidCount);
        }


        [TestMethod]
        public void TestGetAll_PropertyListingManager()
        {
            var propertyListingManager = new PropertyListingManager();

            propertyListingManager.RemoveAllPropertyListings();

            var testPropertyListing1 = new PropertyListing
            {
                LandTaxNumber = "123-1234",
                Address = "123 Test St",
                Owner = "Plugh Properties",
                Total = 234
            };

            var testPropertyListing2 = new PropertyListing
            {
                LandTaxNumber = "123-1234",
                Address = "125 Test St",
                Owner = "Plugh Properties",
                Total = 235
            };

            var testPropertyListing3 = new PropertyListing
            {
                LandTaxNumber = "123-1234",
                Address = "128 Test St",
                Owner = "Plugh Properties",
                Total = 236
            };

            propertyListingManager.Save(testPropertyListing1);
            propertyListingManager.Save(testPropertyListing2);
            propertyListingManager.Save(testPropertyListing3);

            var propertyListings = propertyListingManager.GetAllPropertyListings();

            Assert.AreEqual(3, propertyListings.Count);

            propertyListingManager.RemoveAllPropertyListings();
        }
    }
}
