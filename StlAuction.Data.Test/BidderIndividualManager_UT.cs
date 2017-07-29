using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class BidderIndividualManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_BidderIndividualManager()
        {
            // Setup
            var bidderIndividualManager = new BidderIndividualManager();
            bidderIndividualManager.RemoveAllBidderIndividuals();
            var originalBidderCoporationCount = bidderIndividualManager.GetNumberOfBidderIndividuals();

            // Verify Can Add
            var testBidderIndividual = new BidderIndividual
            {
              CityAddress = "St Louis",
              Name = "xyz co.",
              StateAddress = "MO",
              StreetAddress = "123 Test",
              ZipAddress = "63104",
              Email = "test@gmail.com",
              HomePhone = "5551212",
              WorkPhone = "5551213"
            };

            var id = bidderIndividualManager.Save(testBidderIndividual);

            Assert.AreEqual(id, testBidderIndividual.Id);
            Assert.IsTrue(id > 0);

            var newBidderCoporationCount = bidderIndividualManager.GetNumberOfBidderIndividuals();

            Assert.AreEqual(originalBidderCoporationCount + 1, newBidderCoporationCount);
            
            // Test Update

            testBidderIndividual.ZipAddress = "63105";
            bidderIndividualManager.Update(testBidderIndividual);
            var testBannedBidder2 = bidderIndividualManager.GetById(testBidderIndividual.Id);
            Assert.AreEqual(testBannedBidder2.ZipAddress, "63105");

            // Test Remove

            bidderIndividualManager.RemoveBidderIndividual(testBidderIndividual);
            newBidderCoporationCount = bidderIndividualManager.GetNumberOfBidderIndividuals();
            Assert.AreEqual(originalBidderCoporationCount, newBidderCoporationCount);
            
        }
        
        [TestMethod]
        public void TestGetAll_BidderIndividualManager()
        {
            var bidderIndividualManager = new BidderIndividualManager();

            bidderIndividualManager.RemoveAllBidderIndividuals();

            var originalBidderCoporationCount = bidderIndividualManager.GetNumberOfBidderIndividuals();
            Assert.AreEqual(0, originalBidderCoporationCount);

            var testBidderIndividual1 = new BidderIndividual
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "123 Test",
                ZipAddress = "63104",
                Email = "test@gmail.com",
                HomePhone = "5551212",
                WorkPhone = "5551213"
            };

            var testBidderIndividual2 = new BidderIndividual
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "124 Test",
                ZipAddress = "63104",
                Email = "test@gmail.com",
                HomePhone = "5551212",
                WorkPhone = "5551213"
            };

            var testBidderIndividual3 = new BidderIndividual
            {
                CityAddress = "St Louis",
                Name = "xyz co.",
                StateAddress = "MO",
                StreetAddress = "125 Test",
                ZipAddress = "63104",
                Email = "test@gmail.com",
                HomePhone = "5551212",
                WorkPhone = "5551213"
            };

            bidderIndividualManager.Save(testBidderIndividual1);
            bidderIndividualManager.Save(testBidderIndividual2);
            bidderIndividualManager.Save(testBidderIndividual3);

            var bidderCorporations = bidderIndividualManager.GetAllBidderIndividuals();

            Assert.AreEqual(3, bidderCorporations.Count);

            Assert.IsTrue(bidderCorporations.Count(b => b.StreetAddress == "125 Test") == 1);

            bidderIndividualManager.RemoveAllBidderIndividuals();
        }


    }
}
