using Caliburn.Micro;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ForgotPasswordViewModel :Screen
    {
        public async Task SendEmail()
        {
            RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);
            var task = await RestConnection.ExecuteRequestAsync(client, Method.Post, "/reset-password/init", null, null);
            int statusCode = (int)task.StatusCode;
            if(statusCode == 200)
            {

            }
        }
    }
}
