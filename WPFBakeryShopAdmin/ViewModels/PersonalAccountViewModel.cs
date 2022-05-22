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
    public class PersonalAccountViewModel : Screen, IHandle<PersonalAccount>
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private PersonalAccount _personalAccount;
        private PersonalAccount _savedPersonalAccount;
        private List<ItemLanguage> _languageList;
        private bool _editing = false;
        private string _userImageUrl;
        private IEventAggregator _eventAggregator;

        #region Base
        public PersonalAccountViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.AccountRestClient;
            _eventAggregator.SubscribeOnPublishedThread(this);
            LanguageList = Utilities.LanguageList.LIST;
            LoadPage();
            return Task.CompletedTask;
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return Task.CompletedTask;
        }
        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingPageVis = Visibility.Visible;
                var request = new RestRequest("", Method.Get);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var personalAccount = respone.Result.Content;
                    PersonalAccount = JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
                }
                LoadingPageVis = Visibility.Hidden;
            })).Start();
        }
        public void RequestUpdate()
        {
            Editing = true;
            _savedPersonalAccount = new PersonalAccount(PersonalAccount);
        }
        public void CancelUpdate()
        {
            Editing = false;
            PersonalAccount = new PersonalAccount(_savedPersonalAccount);
        }
        public void UpdateInfo()
        {
            if (!HasError())
            {
                bool thread1Success = false;
                bool thread2Success = false;
                Thread thread1 =
                       new Thread(new ThreadStart(() =>
                       {
                           string result = StringUtils.SerializeObject(PersonalAccount);

                           var response = RestConnection.ExecuteRequestAsync(_restClient, Method.Put, "info", result, "application/json");
                           int statusCode = (int)response.Result.StatusCode;
                           if (statusCode == 200)
                           {
                               thread1Success = true;
                           }
                           else if (statusCode == 400)
                           {
                               ShowFailMessage("Địa chỉ email đã được sử dụng");
                               LoadPage();
                           }
                       }
                       ));

                Thread thread2 =
                       new Thread(new ThreadStart(() =>
                       {
                           if (!UserImageUrl.Contains("http"))
                           {
                               var request = new RestRequest("image", Method.Put);
                               request.AddFile("image", UserImageUrl, "multipart/form-data");
                               var response = _restClient.ExecuteAsync(request);
                               int statusCode = (int)response.Result.StatusCode;
                               if (statusCode == 200)
                               {
                                   thread2Success = true;
                               }
                               else if (statusCode == 400)
                               {
                                   ShowFailMessage("Cập nhật ảnh thất bại");
                                   LoadPage();
                               }
                           }
                           else
                           {
                               thread2Success = true;
                           }
                       }
                       ));

                thread1.Start();
                thread2.Start();

                LoadingPageVis = Visibility.Visible;
                thread1.Join();
                thread2.Join();
                if (thread1Success && thread2Success)
                    ShowSuccessMessage("Cập nhật thông tin thành công");

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

        public Task HandleAsync(PersonalAccount account, CancellationToken cancellationToken)
        {
            PersonalAccount = account;
            return Task.CompletedTask;
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
                if (value != null) UserImageUrl = value.ImageUrl;

                //     _eventAggregator.PublishOnUIThreadAsync(_personalAccount);
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
