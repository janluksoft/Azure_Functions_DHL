using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAPI1
{

    class SecretData
    {
        public string email_sender { set; get; }
        public string email_pass { set; get; }
        public string host { set; get; }
        public int port { set; get; }
        public string email_recipient { set; get; } //destination: cel, miejsce przeznaczenia

        public string ApiKey_EasyPost { set; get; }


        public SecretData()
        {
            InitSecretData();
        }

        private void InitSecretData()
        {
            this.email_sender = "your@email.com";
            this.email_pass = "yourpass";
            this.host = "smtp.yourserver.com";
            this.port = 577;

            this.email_recipient = "recipient@email.com";

            this.ApiKey_EasyPost = GetApiKeyEasyPost();
        }


        #region ApiKey ==================
        private string GetApiKeyEasyPost()
        {
            return ("YourToken_K5c2bde54ab7348d3c4bc89");  // ("EASYPOST_TEST_API_KEY");
        }
        #endregion ApiKey ==================
    }
}
