using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class PersonalAccountViewModel : Screen
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private PersonalAccount _personalAccount;
        private PersonalAccount _savedAccount = null; //Lưu lại để lần sau bấm vào ko cần load lại thông tin account nữa
        private PersonalAccount _accountBeforeEditing = null;
        private List<ItemLanguage> _languageList;
        private bool _editing = false;
        private string _userImageUrl;
        private IEventAggregator _eventAggregator;
        #region Base
        public PersonalAccountViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.AccountRestClient;
            _eventAggregator.SubscribeOnPublishedThread(this);
            LanguageList = Utilities.LanguageList.LIST;

            if (_savedAccount == null)
            {
                await RefreshAccount();
            }
            else
            {
                PersonalAccount = _savedAccount;
            }
            UserImageUrl = PersonalAccount.ImageUrl;

        }

        public async Task RefreshAccount()
        {
            LoadingPageVis = Visibility.Visible;
            PersonalAccount = await GetPersonalAccountFromDBAsync();
            _savedAccount = PersonalAccount;
            LoadingPageVis = Visibility.Hidden;

            await _eventAggregator.PublishOnCurrentThreadAsync(PersonalAccount);
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return Task.CompletedTask;
        }
        public async Task<PersonalAccount> GetPersonalAccountFromDBAsync()
        {
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "", null, null);
            if ((int)response.StatusCode == 200)
            {
                var personalAccount = response.Content;
                return JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
            }
            return null;
        }
        public void RequestUpdate()
        {
            Editing = true;
            _accountBeforeEditing = PersonalAccount.Copy();
        }
        public void CancelUpdate()
        {
            Editing = false;
            PersonalAccount = _accountBeforeEditing.Copy();
        }
        private async Task<bool> UpdateAccountInfoAsync()
        {
            string JSonAccountInfo = StringUtils.SerializeObject(PersonalAccount);

            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Put, "info", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 200)
            {
                return true;
            }
            else if (statusCode == 400)
            {
                ShowFailMessage("Địa chỉ email đã được sử dụng");
                return false;
            }
            return false;
        }
        private async Task<bool> UpdateAccountImageAsync()
        {
            if (!UserImageUrl.Contains("http"))
            {
                var images = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("image", UserImageUrl)
                };
                var response = await RestConnection.ExecuteFileRequestAsync(_restClient, Method.Put, "image", images);
                int statusCode = (int)response.StatusCode;
                if (statusCode == 200)
                {
                    return true;
                }
                else if (statusCode == 400)
                {
                    ShowFailMessage("Cập nhật ảnh thất bại");
                    return false;
                }
                return false;
            }
            else
            {
                return true;
            }
        }
        public async Task UpdatePersonalAccountAsync()
        {
            if (!HasError())
            {
                LoadingPageVis = Visibility.Visible;
                var task1 = UpdateAccountInfoAsync();
                var task2 = UpdateAccountImageAsync();

                var result1 = await task1;
                var result2 = await task2;

                if (result1 == false || result2 == false)
                {
                    PersonalAccount = await GetPersonalAccountFromDBAsync();
                }
                else
                {
                    ShowSuccessMessage("Cập nhật thông tin thành công");
                }
                await _eventAggregator.PublishOnUIThreadAsync(PersonalAccount);
                Editing = false;
                LoadingPageVis = Visibility.Hidden;
            }
        }

        public void UpdatePreviewImage()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
            open.Multiselect = false;
            if ((bool)open.ShowDialog())
            {
                FileInfo fi = new FileInfo(open.FileName);
                float fileSizeInMb = (float)fi.Length / 1000000;
                if (fileSizeInMb <= 1)
                {
                    UserImageUrl = open.FileName;
                }
                else
                {
                    ShowFailMessage("Vui lòng chọn file có kích thước nhỏ hơn");
                }
            }
        }
        private bool HasError()
        {
            return !StringUtils.IsValidEmail(PersonalAccount.Email) ||
                   !StringUtils.IsValidPhoneNumber(PersonalAccount.Phone) ||
                   string.IsNullOrEmpty(PersonalAccount.FirstName) ||
                   string.IsNullOrEmpty(PersonalAccount.LastName) ||
                   string.IsNullOrEmpty(PersonalAccount.Email) ||
                   string.IsNullOrEmpty(PersonalAccount.Phone);
        }
        #endregion

        #region Show Messages
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
        public PersonalAccountView View
        {
            get
            { return (PersonalAccountView)this.GetView(); }
        }
        public Snackbar GreenSB
        {
            get { return View.GreenSB; }
        }
        public Snackbar RedSB
        {
            get { return View.RedSB; }
        }
        #endregion

        #region Binding Properties
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
        public bool Editing
        {
            get { return _editing; }
            set
            {
                _editing = value;
                NotifyOfPropertyChange(() => Editing);
                NotifyOfPropertyChange(() => NotEditing);
            }
        }
        public string UserImageUrl
        {
            get { return _userImageUrl; }
            set
            {
                _userImageUrl = value;
                NotifyOfPropertyChange(() => UserImageUrl);
            }
        }

        private ItemLanguage _selectedItemLanguage;



        public ItemLanguage SelectedItemLanguage
        {
            get
            {
                return _selectedItemLanguage;
            }
            set
            {
                _selectedItemLanguage = value;
                if (value != null)
                    PersonalAccount.LangKey = value.LangKey;
            }
        }
        public bool NotEditing
        {
            get { return !Editing; }
        }
        #endregion
    }
}
