using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class AccountViewModel : Screen
    {
        private RestClient _restClient = RestConnection.ManagementRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private BindableCollection<RowItemAccount> _rowItemAccounts;
        private RowItemAccount _selectedAccount;
        public Pagination _pagination;

        private IWindowManager _windowManager;
        #region Base
        public AccountViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10);
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            LoadPage();
            return Task.CompletedTask;
        }
        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                if (RowItemAccounts != null)
                    RowItemAccounts.Clear();

                LoadingPageVis = Visibility.Visible;

                //   if (_currentPage > _maxPageIndex) _currentPage = 0;
                var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", 10.ToString()),
                };
                var response = RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "accounts", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var accounts = response.Result.Content;
                    RowItemAccounts = JsonConvert.DeserializeObject<BindableCollection<RowItemAccount>>(accounts);
                    Pagination.UpdateStatus(response.Result.Headers);
                }
                NotifyOfPropertyChange(() => Pagination);
                LoadingPageVis = Visibility.Hidden;
            })).Start();
        }

        public void ShowAddingAccountDialog()
        {
            _windowManager.ShowDialogAsync(new AddingAccountViewModel());
        }

        public Pagination Pagination
        {
            get
            {
                return _pagination;
            }
            set
            {
                _pagination = value;
                NotifyOfPropertyChange(() => Pagination);
            }
        }
        #endregion

        #region Pagination
        public void LoadFirstPage()
        {
            Pagination.LoadFirstPage();
            LoadPage();
        }
        public void LoadPreviousPage()
        {
            Pagination.LoadPreviousPage();
            LoadPage();
        }
        public void LoadNextPage()
        {
            Pagination.LoadNextPage();
            LoadPage();
        }
        public void LoadLastPage()
        {
            Pagination.LoadLastPage();
            LoadPage();
        }
        #endregion
        #region Binding Properties
        public RowItemAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyOfPropertyChange(() => SelectedAccount);
            }
        }
        public BindableCollection<RowItemAccount> RowItemAccounts
        {
            get
            {
                return _rowItemAccounts;
            }
            set
            {
                _rowItemAccounts = value;
                NotifyOfPropertyChange(() => RowItemAccounts);
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
        #endregion



    }
}
