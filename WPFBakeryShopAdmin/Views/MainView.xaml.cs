using System.Windows;

namespace WPFBakeryShopAdmin.Views
{
    /// <summary>
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();

        }

        private void LanguageList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Utilities.LanguageList.SwitchLanguage(LanguageList.SelectedIndex);
        }
    }
}
