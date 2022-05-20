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
        private readonly RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private BindableCollection<RowItemAccount> _rowItemAccounts;
        private string _pageIndicator;
        private int _totalCount;
        private RowItemAccount _selectedAccount;
        private readonly int _pageSize = 10;
        private int _currentPage = 0;
        private int _maxPageIndex;
        private bool _couldLoadFirstPage = false, _couldLoadPreviousPage = false, _couldLoadNextPage = false, _couldLoadLastPage = false;
        public AccountViewModel() : base()
        {
            this._restClient = RestConnection.ADMIN_REST_CLIENT;
            LoadPage();
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


        private void UpdateStatus(IReadOnlyCollection<RestSharp.HeaderParameter> headers)
        {
            bool linkDone = false, totalCountDone = false;
            foreach (var header in headers)
            {
                if (header.Name.Equals("X-Total-Count"))
                {
                    _totalCount = (Int32.Parse(header.Value.ToString()));
                    _maxPageIndex = _totalCount / _pageSize;
                    UpdatePageIndicator();
                    totalCountDone = true;
                }

                if (header.Name.Equals("Link"))
                {
                    string value = header.Value.ToString();
                    if (value.Contains("next")) CouldLoadNextPage = true;
                    else CouldLoadNextPage = false;

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

        private void UpdatePageIndicator()
        {
            int start = _pageSize * _currentPage + 1;
            int end = _currentPage == _maxPageIndex ? _totalCount : _pageSize * (_currentPage + 1);
            PageIndicator = $"{start} - {end} của {_totalCount}";
        }

        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                if (RowItemAccounts != null)
                {
                    RowItemAccounts.Clear();
                }
                LoadingPageVis = Visibility.Visible;
                var request = new RestRequest("accounts", Method.Get);

                if (_currentPage > _maxPageIndex) _currentPage = 0;
                request.AddParameter("page", _currentPage).AddParameter("size", _pageSize);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var accounts = respone.Result.Content;
                    RowItemAccounts = JsonConvert.DeserializeObject<BindableCollection<RowItemAccount>>(accounts);
                    UpdateStatus(respone.Result.Headers);
                }
                LoadingPageVis = Visibility.Hidden;
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
            _currentPage = _maxPageIndex;
            LoadPage();
        }

        public string PageIndicator
        {
            get { return _pageIndicator; }
            set
            {
                _pageIndicator = value;
                NotifyOfPropertyChange(() => PageIndicator);

            }
        }
    }
}
