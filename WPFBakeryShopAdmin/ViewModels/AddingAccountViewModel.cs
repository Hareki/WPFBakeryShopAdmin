using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class AddingAccountViewModel : Screen
    {
        private RestClient _restClient;
        private List<ItemLanguage> _languageList;
        private List<ItemRole> _roleList;
        private ItemLanguage _selectedItemLanguage;
        private PersonalAccount _personalAccount;

        #region Base
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.AccountRestClient;
            LanguageList = Utilities.LanguageList.LIST;
            RoleList = Utilities.RoleList.LIST;
            PersonalAccount = new PersonalAccount();
            return Task.CompletedTask;
        }
        public async Task AddNewAccount()
        {
            string JSonAccountInfo = StringUtils.SerializeObject(PersonalAccount);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "accounts", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 201)
            {
                ShowSuccessMessage("Tạo tài khoản thành công, vui lòng kiểm tra email");
            }
            else if (statusCode == 400)
            {
                ShowFailMessage("Tạo tài khoản thất bại, email đã được sử dụng");
            }
            else
            {
                ShowFailMessage("Xảy ra lỗi khi tạo tài khoản");
            }
        }
        private void SetPersonalAccountAuths()
        {

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

        #region Binding Properties
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
        public List<ItemLanguage> LanguageList
        {
            get
            {
                return _languageList;
            }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        public List<ItemRole> RoleList
        {
            get
            {
                return _roleList;
            }
            set
            {
                _roleList = value;
                NotifyOfPropertyChange(() => RoleList);
            }
        }
        public PersonalAccount PersonalAccount
        {
            get
            {
                return _personalAccount;
            }
            set
            {
                _personalAccount = value;
                NotifyOfPropertyChange(() => PersonalAccount);
            }
        }
        #endregion

        #region Mapping Properties
        public AddingAccountView View
        {
            get
            { return (AddingAccountView)this.GetView(); }
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

    }
}
