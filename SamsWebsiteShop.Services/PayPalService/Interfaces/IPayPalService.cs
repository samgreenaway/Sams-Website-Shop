 using SamsWebsiteShop.Services.PayPalService.Model;

namespace SamsWebsiteShop.Services.PayPalService.Interfaces
{
    public interface IPayPalService
    {
        PayPalTransactionModel GetTransactionData(string txToken);


    }
}
