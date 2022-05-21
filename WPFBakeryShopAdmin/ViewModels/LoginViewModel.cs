using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
        private Visibility _loadingPageVis = Visibility.Visible;
        private LoginRequestBody _loginInfo;

        #region Base
        public LoginViewModel() : base()
        {
            LanguageList = Utilities.LanguageList.LIST;
            LoginInfo = new LoginRequestBody("admin@gmail.com", "123456", true);
        }
        public void Login()
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingPageVis = Visibility.Visible;
                RestClient client = new RestClient(RestConnection.AUTHENTICATE_BASE_CONNECTION_STRING);

                string requestBody = StringUtils.SerializeObject(LoginInfo);
                var request = new RestRequest("authenticate", Method.Post);
                //request.AddHeader("Content-type", "application/json");

                request.AddBody(requestBody, contentType: "application/json");
                var respone = client.ExecuteAsync(request);

                int statusCode = (int)respone.Result.StatusCode;
                if (statusCode == 200)
                {
                    var tokenJSon = respone.Result.Content;
                    Token token = JsonConvert.DeserializeObject<Token>(tokenJSon);
                    RestConnection.EstablishConnection(token.IdToken);

                    Properties.Settings.Default.token = token.IdToken;
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
            })).Start();


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
                RedSB.Message.Content,
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
