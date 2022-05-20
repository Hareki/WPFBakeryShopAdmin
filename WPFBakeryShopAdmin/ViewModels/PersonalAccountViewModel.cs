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
using Newtonsoft.Json.Serialization;
using System.ComponentModel;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using System.Collections;
using System.Linq;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class PersonalAccountViewModel : Screen, INotifyDataErrorInfo
    {
        private readonly RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private PersonalAccount _personalAccount;
        private List<ItemLanguage> _languageList;

        public string Test { get; set; }

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

        public void UpdateInfo()
        {
            //new Thread(new ThreadStart(() =>
            //{
            //    var request = new RestRequest("account", Method.Put);
            //    JsonConvert.SerializeObject
            //    var respone = _restClient.ExecuteAsync(request);
            //    if ((int)respone.Result.StatusCode == 200)
            //    {
            //        var personalAccount = respone.Result.Content;
            //        PersonalAccount = JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
            //    }
            //})).Start();

            //string result = StringUtils.SerializeObject(PersonalAccount);
            //string result2 = JsonConvert.SerializeObject(PersonalAccount);
            //Console.WriteLine("result: " + result);
            //Console.WriteLine("result2: " + result2);

            Validate("Test", false, "Email không được để trống");
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

        #region Validation
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        private readonly Dictionary<string, List<object>> _ValidationErrorsByProperty =
            new Dictionary<string, List<object>>();
        public bool HasErrors => _ValidationErrorsByProperty.Any();
        public IEnumerable GetErrors(string propertyName)
        {
            if (_ValidationErrorsByProperty.TryGetValue(propertyName, out List<object> errors))
            {
                return errors;
            }
            return Array.Empty<object>();
        }

        private void Validate(string changedPropertyName, bool valid, string notValidError)
        {
            if (valid && _ValidationErrorsByProperty.Remove(changedPropertyName))
            {
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(changedPropertyName));
            }
            else
            {
                _ValidationErrorsByProperty[changedPropertyName] = new List<object> { notValidError };
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(changedPropertyName));
            }
        }
        #endregion
    }
}
