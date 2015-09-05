using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using SamsWebsiteShop.Services.PayPalService.Interfaces;
using SamsWebsiteShop.Services.PayPalService.Model;

namespace SamsWebsiteShop.Services.PayPalService
{
    public class PayPalService : IPayPalService
    {
        private string AuthToken
        {
            get
            {
                return ConfigurationManager.AppSettings["PayPalAuthToken"];
            }
        }

        private string Uri
        {
            get
            {
                return ConfigurationManager.AppSettings["PayPalUri"];
            }
        }

        public PayPalTransactionModel GetTransactionData(string txToken)
        {
            var objToReturn = new PayPalTransactionModel();
            var query = string.Format("cmd=_notify-synch&tx={0}&at={1}", txToken, AuthToken);
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(Uri);

            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = query.Length;

            //Send the request to PayPal and get the response - not using a try / catch here as we want to see a hard error if the API fails.
            StreamWriter streamOut = new StreamWriter(req.GetRequestStream(), Encoding.ASCII);
            streamOut.Write(query);
            streamOut.Close();
            StreamReader streamIn = new StreamReader(req.GetResponse().GetResponseStream());
            string strResponse = streamIn.ReadToEnd();
            streamIn.Close();

            Dictionary<string, string> results = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(strResponse))
            {
                var reader = new StringReader(strResponse);
                var line = reader.ReadLine();

                switch (line)
                {
                    case "SUCCESS":

                        while ((line = reader.ReadLine()) != null)
                        {
                            objToReturn.Properties.Add(line.Split('=')[0], line.Split('=')[1]);
                        }
                        if (objToReturn.Properties.Count != 0)
                        {
                            objToReturn.IsPopulated = true;
                            objToReturn.State = TransactionState.Success;
                        }
                        break;
                    case "FAIL":
                        objToReturn.State = TransactionState.Failure;
                        break;
                }
            }
            else
            {
                objToReturn.State = TransactionState.Error;
            }
            return objToReturn;
        }
    }
}
