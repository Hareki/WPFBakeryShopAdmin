using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFBakeryShopAdmin.Utilities
{
    public class Program
    {
        public static void Logout()
        {
            RestConnection.BearerToken = string.Empty;
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
