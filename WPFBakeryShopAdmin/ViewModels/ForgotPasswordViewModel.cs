using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ForgotPasswordViewModel : Screen
    {
        public async Task SendEmail()
        {
            //RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);
            //var task = await RestConnection.ExecuteRequestAsync(client, Method.Post, "/reset-password/init", null, null);
            //int statusCode = (int)task.StatusCode;
            //if (statusCode == 200)
            //{
            ForgotPasswordView view = (ForgotPasswordView)this.GetView();
            //view.Dispatcher.Invoke(() =>
            //{
            //    view.DialogContainer.IsOpen = true;
            //});
            //}
            var result = await DialogHost.Show(view.DialogContent);
            bool shouldContinue = System.Convert.ToBoolean(result);
            if (shouldContinue)
            {
                Console.WriteLine("HAHAHAH");
            }
            else
            {
                Console.WriteLine("BBBBBBBBBBBBBBB");
            }

        }
    }
}
