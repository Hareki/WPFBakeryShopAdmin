using Caliburn.Micro;
using System.Windows;
using WPFBakeryShopAdmin.ViewModels;

namespace WPFBakeryShopAdmin
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<LoginViewModel>();
        }
    }
}
