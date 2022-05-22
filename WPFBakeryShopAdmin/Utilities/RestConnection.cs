﻿using RestSharp;
using RestSharp.Authenticators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Utilities
{
    public class RestConnection
    {
        public static readonly string ADMIN_BASE_CONNECTION_STRING = "http://localhost:8080/api/admin/";
        public static readonly string ACCOUNT_BASE_CONNECTION_STRING = "http://localhost:8080/api/account/";
        public static readonly string AUTHENTICATE_BASE_CONNECTION_STRING = "http://localhost:8080/api/";
        //public static readonly string AUTHENTICATE_BASE_CONNECTION_STRING = "https://bakeryshop-web-service.herokuapp.com/api/";

        public static string BearerToken;
        private static RestClient _managementRestClient;
        private static RestClient _accountRestClient;
        private static RestClient _authRestClient;

        #region Base
        public static void EstablishConnection(string token)
        {
            BearerToken = token;

            ManagementRestClient = new RestClient(ADMIN_BASE_CONNECTION_STRING);
            AccountRestClient = new RestClient(ACCOUNT_BASE_CONNECTION_STRING);
            AuthRestClient = new RestClient(AUTHENTICATE_BASE_CONNECTION_STRING);
        }
        public static Task<RestResponse> ExecuteRequestAsync(RestClient restClient, Method method, string requestURl, string requestBody, string contentType)
        {
            var request = new RestRequest(requestURl, method);
            if (!string.IsNullOrEmpty(requestBody))
                request.AddBody(requestBody, contentType);
            return restClient.ExecuteAsync(request);
        }
        public static Task<RestResponse> ExecuteRequestAsync(RestClient restClient, Method method, string requestURl, List<KeyValuePair<string, string>> parameters)
        {
            var request = new RestRequest(requestURl, method);
            if (parameters != null && parameters.Count > 0)
            {
                foreach (KeyValuePair<string, string> element in parameters)
                {
                    request.AddParameter(element.Key, element.Value);
                }
            }
            return restClient.ExecuteAsync(request);
        }
        #endregion

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
        public static RestClient AuthRestClient
        {
            get
            { return _authRestClient; }
            set
            {
                _authRestClient = value;
                _authRestClient.Authenticator = new JwtAuthenticator(BearerToken);
            }
        }
        #endregion
    }
}
