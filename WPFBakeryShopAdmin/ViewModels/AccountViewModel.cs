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
    public class AccountViewModel : Screen
    {
        private RestClient RestClient;
        private Visibility _loadingVisibility = Visibility.Visible;
        private BindableCollection<RowItemAccount> _rowItemAccounts;
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



        public Visibility LoadingVisibility
        {
            get { return _loadingVisibility; }
            set
            {
                _loadingVisibility = value;
                NotifyOfPropertyChange(() => LoadingVisibility);
            }
        }

        private RowItemAccount _selectedAccount;
        private readonly int _pageSize = 12;
        private int _currentPage = 0;
        private int _maxPage;
        private bool _couldLoadFirstPage = false, _couldLoadPreviousPage = false, _couldLoadNextPage = false, _couldLoadLastPage = false;

        public bool CouldLoadFirstPage
        {
            get { return _couldLoadFirstPage; }
            set
            {
                _couldLoadFirstPage = value;
                NotifyOfPropertyChange(() => CouldLoadFirstPage);
            }
        }

        public bool CouldLoadPreviousPage
        {
            get { return _couldLoadPreviousPage; }
            set
            {
                _couldLoadPreviousPage = value;
                NotifyOfPropertyChange(() => CouldLoadPreviousPage);
            }
        }

        public bool CouldLoadNextPage
        {
            get { return _couldLoadNextPage; }
            set
            {
                _couldLoadNextPage = value;
                NotifyOfPropertyChange(() => CouldLoadNextPage);
            }
        }

        public bool CouldLoadLastPage
        {
            get { return _couldLoadLastPage; }
            set
            {
                _couldLoadLastPage = value;
                NotifyOfPropertyChange(() => CouldLoadLastPage);
            }
        }



        public RowItemAccount SelectedAccount
        {
            get { return _selectedAccount; }
            set
            {
                _selectedAccount = value;
                NotifyOfPropertyChange(() => SelectedAccount);
            }
        }

        public AccountViewModel() : base()
        {
            this.RestClient = new RestClient(RestConnection.BASE_CONNECTION_STRING);
            this.RestClient.Authenticator = new JwtAuthenticator(RestConnection.BearerToken);
            LoadPage();
        }

        private void UpdateStatus(IReadOnlyCollection<RestSharp.HeaderParameter> headers)
        {
            bool linkDone = false, totalCountDone = false;
            foreach (var header in headers)
            {
                if (header.Name.Equals("X-Total-Count"))
                {
                    _maxPage = (Int32.Parse(header.Value.ToString()) / _pageSize);
                    totalCountDone = true;
                }

                if (header.Name.Equals("Link"))
                {
                    string value = header.Value.ToString();
                    if (value.Contains("next")) CouldLoadNextPage = true;
                    else CouldLoadNextPage = true;

                    if (value.Contains("prev")) CouldLoadPreviousPage = true;
                    else CouldLoadPreviousPage = false;

                    if (value.Contains("first")) CouldLoadFirstPage = true;
                    else CouldLoadFirstPage = false;

                    if (value.Contains("last")) CouldLoadLastPage = true;
                    else CouldLoadLastPage = false;

                    linkDone = true;
                }
                if (totalCountDone && linkDone) return;
            }
        }
        private void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                if (RowItemAccounts != null)
                {
                    RowItemAccounts.Clear();
                }
                LoadingVisibility = Visibility.Visible;
                var request = new RestRequest("accounts", Method.Get);
                request.AddParameter("page", _currentPage).AddParameter("size", _pageSize);
                var respone = RestClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var accounts = respone.Result.Content;
                    RowItemAccounts = JsonConvert.DeserializeObject<BindableCollection<RowItemAccount>>(accounts);
                    UpdateStatus(respone.Result.Headers);
                }
                LoadingVisibility = Visibility.Hidden;
            })).Start();

        }
        public void LoadFirstPage()
        {
            _currentPage = 0;
            LoadPage();
        }
        public void LoadPreviousPage()
        {
            _currentPage--;
            LoadPage();
        }
        public void LoadNextPage()
        {
            _currentPage++;
            LoadPage();
        }

        public void LoadLastPage()
        {
            _currentPage = _maxPage;
            LoadPage();
        }



    }
}
