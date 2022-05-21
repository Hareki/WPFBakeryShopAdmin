using RestSharp;
using RestSharp.Authenticators;

namespace WPFBakeryShopAdmin.Utilities
{
    public class RestConnection
    {
        public static readonly string ADMIN_BASE_CONNECTION_STRING = "http://localhost:8080/api/admin/";
        public static readonly string ACCOUNT_BASE_CONNECTION_STRING = "http://localhost:8080/api/account/";
        public static string BearerToken = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJhZG1pbkBnbWFpbC5jb20iLCJhdXRoIjoiUk9MRV9BRE1JTixST0xFX1VTRVIiLCJleHAiOjE2NTMxMzA5NzV9.LahrZlf8MeY1Ehv4g0Jh0OUWlPYm4JXWJ3FbSPmfQuL8VlMQ3cms-6TDRvXNiEeHp457TusVl79virTYUF6-Hg";
        //TOKEN HEROKU eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJhZG1pbkBnbWFpbC5jb20iLCJhdXRoIjoiUk9MRV9BRE1JTixST0xFX1VTRVIiLCJleHAiOjE2NTI3MTM3MDV9.xJY7fUzZbVuQOM-KF3iNE8S9brOLSdOlrQBECGb_p30k-oYB_jAOCrq25nSHwtcW_Atx8ZvsfW-IW6hp49pw6A
        //TOKEN LOCAL eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJhZG1pbkBnbWFpbC5jb20iLCJhdXRoIjoiUk9MRV9BRE1JTixST0xFX1VTRVIiLCJleHAiOjE2NTI3NTAyNjR9.H1YJOtQ0DzxNl1hfwXsWwRUT27fTTC1Rx4eHr2fwOYQHGAweVuTB0gX6mpiKko8G_0VXr-w_jf6dvPmJSSFRUw
        public static readonly RestClient ADMIN_REST_CLIENT;
        static RestConnection()
        {
            ADMIN_REST_CLIENT = new RestClient(RestConnection.ADMIN_BASE_CONNECTION_STRING);
            ADMIN_REST_CLIENT.Authenticator = new JwtAuthenticator(RestConnection.BearerToken);
        }
    }
}
