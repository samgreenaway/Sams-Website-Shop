using System;
using System.Collections.Generic;
using System.Web;

namespace SamsWebsiteShop.Services.PayPalService.Model
{
    public class PayPalTransactionModel
    {
        public Dictionary<string, string> Properties { get; set; }
        public TransactionState State { get; set; }
        public bool IsPopulated { get; set; }

        //Fields we are going to use in the view or send to our own database
        //These are readonly and only available after populating the object via the service.
        public string TransactionDateTime
        {
            get { return GetPropertyValue("payment_date"); }
        }
        
        public string ItemUniqueId
        {
            get { return GetPropertyValue("item_number"); }
        }

        public string TransactionId
        {
            get { return GetPropertyValue("txn_id"); }
        }

        public string FirstName
        {
            get { return GetPropertyValue("first_name"); }
        }

        public string LastName
        {
            get { return GetPropertyValue("last_name"); }
        }

        public string Item
        {
            get { return GetPropertyValue("item_name"); }
        }

        public string InvoiceTotal
        {
            get { return GetPropertyValue("mc_gross"); }
        }

        public string EmailAddress
        {
            get { return GetPropertyValue("payer_email"); }
        }


        public string AddressStreet
        {
            get { return GetPropertyValue("address_street"); }
        }

        public string AddressCity
        {
            get { return GetPropertyValue("address_city"); }
        }

        public string AddressCounty
        {
            get { return GetPropertyValue("address_state"); }
        }

        public string AddressCountry
        {
            get { return GetPropertyValue("address_country"); }
        }

        public string AddressPostCode
        {
            get { return GetPropertyValue("address_zip"); }
        }



        public PayPalTransactionModel()
        {
            Properties = new Dictionary<string, string>();
        }

        private string GetPropertyValue(string propName)
        {
            return IsPopulated && Properties.ContainsKey(propName)
                    ? HttpUtility.UrlDecode(Properties[propName])
                    : string.Empty;
        }
    }

    public enum TransactionState
    {
        Success = 1,
        Failure = 2,
        Error = 3
    }
}
