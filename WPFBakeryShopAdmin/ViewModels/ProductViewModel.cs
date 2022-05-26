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
    public class ProductViewModel : Conductor<Screen>.Collection.OneActive
    {
        private RestClient _restClient = RestConnection.ManagementRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingInfoVis = Visibility.Hidden;
        private BindableCollection<RowItemProduct> _rowItemProducts;
        private string _pageIndicator;
        private int _totalCount;
        private readonly int _pageSize = 10;
        private int _currentPage = 0;
        private int _maxPageIndex;
        private bool _couldLoadFirstPage = false, _couldLoadPreviousPage = false, _couldLoadNextPage = false, _couldLoadLastPage = false;
        private IWindowManager _windowManager;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            LoadPage(-1);
            return Task.CompletedTask;
        }
        public void LoadPage(int page)
        {
            if (page != -1) _currentPage = page;
            new Thread(new ThreadStart(() =>
            {
                if (RowItemProducts != null)
                    RowItemProducts.Clear();

                LoadingPageVis = Visibility.Visible;
                if (_currentPage > _maxPageIndex) _currentPage = 0;
                var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", _currentPage.ToString()),
                      new KeyValuePair<string, string>("size", _pageSize.ToString()),
                };
                var response = RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "products", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var products = response.Result.Content;
                    RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<RowItemProduct>>(products);
                    UpdatePageStatus(response.Result.Headers);
                }
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        private void UpdatePageStatus(IReadOnlyCollection<RestSharp.HeaderParameter> headers)
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
        #endregion

        #region Binding Properties
        public BindableCollection<RowItemProduct> RowItemProducts
        {
            get
            {
                return _rowItemProducts;
            }
            set
            {
                _rowItemProducts = value;
                NotifyOfPropertyChange(() => RowItemProducts);
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
        public Visibility LoadingInfoVis
        {
            get { return _loadingInfoVis; }
            set
            {
                _loadingInfoVis = value;
                NotifyOfPropertyChange(() => LoadingInfoVis);
            }
        }
        #endregion

        #region Pagination
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
        public string PageIndicator
        {
            get { return _pageIndicator; }
            set
            {
                _pageIndicator = value;
                NotifyOfPropertyChange(() => PageIndicator);
            }
        }
        public void LoadFirstPage()
        {
            _currentPage = 0;
            LoadPage(-1);
        }
        public void LoadPreviousPage()
        {
            _currentPage--;
            LoadPage(-1);
        }
        public void LoadNextPage()
        {
            _currentPage++;
            LoadPage(-1);
        }
        public void LoadLastPage()
        {
            _currentPage = _maxPageIndex;
            LoadPage(-1);
        }
        private void UpdatePageIndicator()
        {
            int start = _pageSize * _currentPage + 1;
            int end = _currentPage == _maxPageIndex ? _totalCount : _pageSize * (_currentPage + 1);
            PageIndicator = $"{start} - {end} của {_totalCount}";
        }
        #endregion
    }
}
