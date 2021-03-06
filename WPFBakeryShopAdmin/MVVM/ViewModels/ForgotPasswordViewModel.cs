using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System.Threading.Tasks;
using System.Windows.Input;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ForgotPasswordViewModel : Screen
    {
        private string _email;
        private readonly LoginViewModel _loginViewModel;
        private readonly IWindowManager _windowManager;

        #region Base
        public ForgotPasswordViewModel()
        {
            Email = "ASDAS";
        }
        public ForgotPasswordViewModel(LoginViewModel loginViewModel, IWindowManager windowManager)
        {
            _loginViewModel = loginViewModel;
            _windowManager = windowManager;
        }
        public async Task SendEmail()
        {
            if (HasErrors()) return;

            RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);
            var task = await RestConnection.ExecuteRequestAsync(client, Method.Post, "/reset-password/init", Email, "text/plain");
            int statusCode = (int)task.StatusCode;
            if (statusCode == 200)
            {
                SetContentVisible(Content.SUCCESS);
                await DialogHost.Show(View.DialogContent);
                await GoBackToLoginForm();
            }
            else
            {
                SetContentVisible(Content.FAIL);
                await DialogHost.Show(View.DialogContent);
            }
        }
        public async Task SendEmail(ActionExecutionContext context)
        {
            KeyEventArgs keyArgs = context.EventArgs as KeyEventArgs;

            if (keyArgs != null && keyArgs.Key == Key.Enter)
            {
                await SendEmail();
            }
        }
        public async Task Cancel()
        {
            await GoBackToLoginForm();
        }
        private async Task GoBackToLoginForm()
        {
            await this._windowManager.ShowWindowAsync(_loginViewModel);
            await this.DeactivateAsync(true);
        }
        private bool HasErrors()
        {
            return string.IsNullOrEmpty(Email) || !StringUtils.IsValidEmail(Email);
        }
        private void SetContentVisible(Content content)
        {
            if (content == Content.SUCCESS)
            {
                View.FailContent.Visibility = System.Windows.Visibility.Collapsed;
                View.SuccessContent.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                View.FailContent.Visibility = System.Windows.Visibility.Visible;
                View.SuccessContent.Visibility = System.Windows.Visibility.Collapsed;
            }
        }
        #endregion

        #region Properties
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                NotifyOfPropertyChange(() => Email);
            }
        }
        public ForgotPasswordView View
        {
            get
            {
                return (ForgotPasswordView)this.GetView();
            }
        }
        #endregion

    }
    enum Content
    {
        SUCCESS, FAIL
    }
}
