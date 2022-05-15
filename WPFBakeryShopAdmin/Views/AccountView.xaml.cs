using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPFBakeryShopAdmin.Views
{
    /// <summary>
    /// Interaction logic for AccountView.xaml
    /// </summary>
    public partial class AccountView : UserControl
    {
        public AccountView()
        {
            InitializeComponent();
            expander.IsEnabled = expander.IsExpanded = false;

        }
        private void RowItemAccounts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expander.IsEnabled = RowItemAccounts.SelectedIndex >= 0;
            if (expander.IsEnabled == false) expander.IsExpanded = false;
            PBImage.Visibility = Visibility.Visible;
            Console.WriteLine("Vis");

        }

        private void image_Changed(object sender, EventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() => HideLoading()), DispatcherPriority.ContextIdle, null);
        }

        private void HideLoading()
        {
            PBImage.Visibility = Visibility.Hidden;
            Console.WriteLine("Hid");
        }
    }
}
