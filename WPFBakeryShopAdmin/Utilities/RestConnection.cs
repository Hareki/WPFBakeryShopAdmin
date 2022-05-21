using RestSharp;
using RestSharp.Authenticators;

namespace WPFBakeryShopAdmin.Utilities
{
    public class RestConnection
    {
        public static readonly string ADMIN_BASE_CONNECTION_STRING = "http://localhost:8080/api/admin/";
        public static readonly string ACCOUNT_BASE_CONNECTION_STRING = "http://localhost:8080/api/account/";
        public static readonly string AUTHENTICATE_BASE_CONNECTION_STRING = "http://localhost:8080/api/";

        public static string BearerToken;
        private static RestClient _managementRestClient;
        private static RestClient _accountRestClient;

        public static void EstablishConnection(string token)
        {
            BearerToken = token;

            ManagementRestClient = new RestClient(ADMIN_BASE_CONNECTION_STRING);
            AccountRestClient = new RestClient(ACCOUNT_BASE_CONNECTION_STRING);
        }

        #region Properties
        public static RestClient AccountRestClient
        {
            get
            { return _managementRestClient; }
            set
            {
                _managementRestClient = value;
                _managementRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        public static RestClient ManagementRestClient
        {
            get
            { return _accountRestClient; }
            set
            {
                _accountRestClient = value;
                _accountRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        #endregion
    }
}
