using System.Web.Mvc;
using SamsWebsiteShop.Services.PayPalService;
using SamsWebsiteShop.Services.PayPalService.Interfaces;
using SamsWebsiteShop.Services.PayPalService.Model;

namespace SamsWebsiteShop.Web.Controllers
{
    public class PayPalCallBackController : Controller
    {
        private IPayPalService PayPalService;

        public PayPalCallBackController(IPayPalService payPalService)
        {
            this.PayPalService = payPalService;
        }

        public ActionResult Complete(string tx)
        {
            var transaction = this.PayPalService.GetTransactionData(tx);
            if (transaction.State == TransactionState.Success)
                return View(transaction);
            return RedirectToAction("Cancel");
        }

        public ActionResult Cancel()
        {
            return View();
        }
    }
}