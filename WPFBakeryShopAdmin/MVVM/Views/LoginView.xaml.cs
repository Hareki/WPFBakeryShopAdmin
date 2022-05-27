using System.Windows;

namespace WPFBakeryShopAdmin.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Window
    {
        public LoginView()
        {
            InitializeComponent();
            LanguageList.SelectedIndex = 0;
        }

        private void LanguageList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            Lists.LanguageList.SwitchLanguage(LanguageList.SelectedIndex);
        }
    }
}
