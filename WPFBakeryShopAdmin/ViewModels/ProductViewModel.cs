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
        private RowItemProduct _selectedProduct;
        private Pagination _pagination;

        private IWindowManager _windowManager;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
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
                if (RowItemProducts != null)
                    RowItemProducts.Clear();

                LoadingPageVis = Visibility.Visible;
                var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", Pagination.PageSize.ToString()),
                };
                var response = RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "products", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var products = response.Result.Content;
                    RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<RowItemProduct>>(products);
                    Pagination.UpdatePaginationStatus(response.Result.Headers);
                }
                NotifyOfPropertyChange(() => Pagination);
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        public void RowItemProducts_SelectionChanged()
        {

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
        public RowItemProduct SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                NotifyOfPropertyChange(() => SelectedProduct);
            }
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
    }
}
