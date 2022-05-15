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

namespace WPFBakeryShopAdmin.Views
{
    /// <summary>
    /// Interaction logic for BillView.xaml
    /// </summary>
    public partial class BillView : UserControl
    {
        public BillView()
        {
            InitializeComponent();
            expander.IsEnabled = expander.IsExpanded = false;
        }
        private void RowItemBills_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            expander.IsEnabled = RowItemBills.SelectedIndex >= 0;
            if (expander.IsEnabled == false) expander.IsExpanded = false;
        }
    }
}
