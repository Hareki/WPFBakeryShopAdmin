using Caliburn.Micro;
using RestSharp;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Models;
using Newtonsoft.Json;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Threading;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class PersonalAccountViewModel : Screen
    {
        private readonly RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private PersonalAccount _personalAccount;
        private List<ItemLanguage> _languageList;

        public PersonalAccountViewModel() : base()
        {
            _restClient = new RestClient(RestConnection.ACCOUNT_BASE_CONNECTION_STRING);
            _restClient.Authenticator = new JwtAuthenticator(RestConnection.BearerToken);
            LanguageList = Utilities.LanguageList.LIST;
            LoadPage();
        }

        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingPageVis = Visibility.Visible;
                var request = new RestRequest("account", Method.Get);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var personalAccount = respone.Result.Content;
                    PersonalAccount = JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
                }
                LoadingPageVis = Visibility.Hidden;
            })).Start();
        }

        public PersonalAccount PersonalAccount
        {
            get { return _personalAccount; }
            set
            {
                _personalAccount = value;
                NotifyOfPropertyChange(() => PersonalAccount);
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
        public List<ItemLanguage> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }

    }
}
