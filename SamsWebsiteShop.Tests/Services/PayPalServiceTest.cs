using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamsWebsiteShop.Services.PayPalService;
using SamsWebsiteShop.Services.PayPalService.Model;

namespace SamsWebsiteShop.Tests.Services
{
    [TestClass]
    public class PayPalServiceTest
    {
        /// <summary>
        /// Test the get transaction data method with a known transaction
        /// </summary>
        [TestMethod]
        public void TestIntegrationSuccess()
        {
            var service = new PayPalService();
            var transaction = service.GetTransactionData("0X158626EH703433F");
            Assert.AreEqual(true, transaction.IsPopulated);
        }

        /// <summary>
        /// Test the get transaction data method with a transaction id known to be wrong
        /// </summary>
        [TestMethod]
        public void TestIntegrationFailure()
        {
            var service = new PayPalService();
            var transaction = service.GetTransactionData("0X158626EH703433FXXX");
            Assert.AreEqual(TransactionState.Failure, transaction.State);
        }
    }
}
