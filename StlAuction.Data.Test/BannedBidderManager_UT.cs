using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StlAuction.Types;

namespace StlAuction.Data.Test
{
    [TestClass]
    public class BannedBidderManager_UT
    {
        [TestMethod]
        public void TestSaveUpdateAndDelete_BannedBidderManager()
        {
            var bannedBidderManager = new BannedBidderManager();

            bannedBidderManager.RemoveAllBannedBidders();

            var originalBannedBidderCount = bannedBidderManager.GetNumberOfBannedBidders();

            var testBannedBidder = new BannedBidder
            {
               Address = "123 Test Street",
               CompanyAndIndividualName = "Xyzzy Company",
               Date = DateTime.Now,
               PhoneNumbers = "555-1212",
               Reason = "Test",
               SuitNumber = "012-234"
            };

            var id = bannedBidderManager.Save(testBannedBidder);

            Assert.AreEqual(id, testBannedBidder.Id);
            Assert.IsTrue(id > 0);

            var newBannedBidderCount = bannedBidderManager.GetNumberOfBannedBidders();

            Assert.AreEqual(originalBannedBidderCount + 1, newBannedBidderCount);
            
            testBannedBidder.Reason = "Test2";

            bannedBidderManager.Update(testBannedBidder);

            var testBannedBidder2 = bannedBidderManager.GetById(testBannedBidder.Id);

            Assert.AreEqual(testBannedBidder2.Reason, "Test2");

            bannedBidderManager.RemoveBannedBidder(testBannedBidder);

            newBannedBidderCount = bannedBidderManager.GetNumberOfBannedBidders();

            Assert.AreEqual(originalBannedBidderCount, newBannedBidderCount);
        }




        [TestMethod]
        public void TestGetAll_BannedBidderManager()
        {
            var bannedBidderManager = new BannedBidderManager();

            bannedBidderManager.RemoveAllBannedBidders();

            var testBannedBidder1 = new BannedBidder
            {
                Address = "123 Test Street",
                CompanyAndIndividualName = "Xyzzy Company",
                Date = DateTime.Now,
                PhoneNumbers = "555-1212",
                Reason = "Test",
                SuitNumber = "012-234"
            };

            var testBannedBidder2 = new BannedBidder
            {
                Address = "123 Plugh Street",
                CompanyAndIndividualName = "Plugh Company",
                Date = DateTime.Now,
                PhoneNumbers = "555-1212",
                Reason = "Test",
                SuitNumber = "012-212"
            };

            var testBannedBidder3 = new BannedBidder
            {
                Address = "123 Plover Street",
                CompanyAndIndividualName = "Plover Company",
                Date = DateTime.Now,
                PhoneNumbers = "555-1214",
                Reason = "Test",
                SuitNumber = "012-244"
            };

            bannedBidderManager.Save(testBannedBidder1);
            bannedBidderManager.Save(testBannedBidder2);
            bannedBidderManager.Save(testBannedBidder3);

            var bannedBidders = bannedBidderManager.GetAllBannedBidders();

            Assert.AreEqual(3, bannedBidders.Count);

            bannedBidderManager.RemoveAllBannedBidders();
        }


    }
}
