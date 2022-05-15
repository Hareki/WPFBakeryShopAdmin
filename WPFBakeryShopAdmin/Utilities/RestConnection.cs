using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Utilities
{
    public class RestConnection
    {
        public static readonly string BASE_CONNECTION_STRING = "http://localhost:8080/api/admin/";
        public static string BearerToken = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJhZG1pbkBnbWFpbC5jb20iLCJhdXRoIjoiUk9MRV9BRE1JTixST0xFX1VTRVIiLCJleHAiOjE2NTI2NjM3MTZ9.H_hkrCnRWOpadYBK6PwAJNFtGarsaR8JyHQuiQzXftEX1QrWjUTK8ab8B8Nn804oga9xU9fZ0G7UymQyvTW6tg";
        public static readonly RestClient REST_CLIENT;
        static RestConnection()
        {
            REST_CLIENT = new RestClient(RestConnection.BASE_CONNECTION_STRING);
            REST_CLIENT.Authenticator = new JwtAuthenticator(RestConnection.BearerToken);
        }
    }
}
