using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class LoginViewModel : Screen
    {
        private List<ItemLanguage> _languageList;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private LoginRequestBody _loginInfo;

        private MainViewModel _mainViewModel;
        private IWindowManager _windowManager;

        #region Base
        public LoginViewModel(MainViewModel mainViewModel, IWindowManager windowManager)
        {
            _mainViewModel = mainViewModel;
            _windowManager = windowManager;
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LanguageList = Utilities.LanguageList.LIST;
            LoginInfo = new LoginRequestBody("admin@gmail.com", "123456", false);
            //Properties.Settings.Default.token = null;
            //Properties.Settings.Default.Save();
            _ = LoginWithSessionAsync();
            return Task.CompletedTask;
        }
        public async Task LoginWithSessionAsync()
        {
            if (!string.IsNullOrEmpty(RestConnection.SavedBearerToken))
            {
                LoadingPageVis = Visibility.Visible;
                var success = await FetchSession();
                if (success)
                {
                    await this._windowManager.ShowWindowAsync(_mainViewModel);
                    await this.TryCloseAsync();
                }
                else
                {
                    ShowFailMessage("Phiên đăng nhập quá hạn");
                    LoadingPageVis = Visibility.Hidden;
                }

            }
        }
        public async Task<bool> FetchSession()
        {
            RestConnection.BearerToken = RestConnection.SavedBearerToken;
            RestConnection.EstablishConnection(RestConnection.BearerToken);
            RestClient auth = RestConnection.AuthRestClient;
            var response = await RestConnection.ExecuteRequestAsync
                (auth, Method.Get, "authenticate", null, null);
            if (!string.IsNullOrEmpty(response.Content))
                return true;

            return false;
        }
        public async Task LoginAsync()
        {
            LoadingPageVis = Visibility.Visible;

            RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);
            string requestBody = StringUtils.SerializeObject(LoginInfo);

            var response = await RestConnection.ExecuteRequestAsync
                (client, Method.Post, "authenticate", requestBody, "application/json");

            int statusCode = (int)response.StatusCode;
            if (statusCode == 200)
            {
                var tokenJSon = response.Content;
                Token token = JsonConvert.DeserializeObject<Token>(tokenJSon);
                RestConnection.EstablishConnection(token.IdToken);
                if (LoginInfo.RememberMe)
                    RestConnection.SavedBearerToken = token.IdToken;

                await this._windowManager.ShowWindowAsync(_mainViewModel);
                await this.TryCloseAsync();
            }
            else if (statusCode == 400 || statusCode == 401)
            {
                ShowFailMessage("Email hoặc mật khẩu không đúng");
            }
            else
            {
                ShowFailMessage("Xảy ra lỗi khi đăng nhập");

            }
            LoadingPageVis = Visibility.Hidden;
        }
        #endregion

        #region Showing Messages
        private void ShowSuccessMessage(string message)
        {
            View.Dispatcher.Invoke(() =>
            {
                View.GreenMessage.Text = message;

                GreenSB.MessageQueue?.Enqueue(
                View.GreenContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        private void ShowFailMessage(string message)
        {
            View.Dispatcher.Invoke(() =>
            {
                View.RedMessage.Text = message;
                RedSB.MessageQueue?.Enqueue(
                View.RedContent,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        #endregion

        #region View Mapping Properties
        public LoginView View
        {
            get
            {
                return (LoginView)this.GetView();
            }
        }
        public Snackbar GreenSB
        {
            get
            {
                return View.GreenSB;
            }
        }
        public Snackbar RedSB
        {
            get
            {
                return View.RedSB;
            }
        }
        #endregion

        #region Properties
        public List<ItemLanguage> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        public Visibility LoadingPageVis
        {
            get { return _loadingPageVis; }
            set
            {
                _loadingPageVis = value;
                NotifyOfPropertyChange(() => LoadingPageVis);
            }
        }
        public LoginRequestBody LoginInfo
        {
            get { return _loginInfo; }
            set
            {
                _loginInfo = value;
                NotifyOfPropertyChange(() => LoginInfo);
            }
        }

        #endregion

    }
}
