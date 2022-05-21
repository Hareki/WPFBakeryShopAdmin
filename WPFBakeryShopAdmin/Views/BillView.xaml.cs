using System.Windows.Controls;

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
            DetailExpander.IsEnabled = DetailExpander.IsExpanded = false;
        }

    }
}
