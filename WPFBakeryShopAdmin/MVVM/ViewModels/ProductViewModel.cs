using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdmin.Interfaces;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ProductViewModel : Screen, IViewModel
    {
        private RestClient _managementRestClient = RestConnection.ManagementRestClient;
        private RestClient _publicRestClient = RestConnection.PublicRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingInfoVis = Visibility.Hidden;
        private BindableCollection<ProductRowItem> _rowItemProducts;
        private ProductRowItem _selectedProduct;
        private ProductDetail _productDetails;
        private Pagination _pagination;
        private IWindowManager _windowManager;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _managementRestClient = RestConnection.ManagementRestClient;
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
                var response = RestConnection.ExecuteParameterRequestAsync(_managementRestClient, Method.Get, "products", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var products = response.Result.Content;
                    RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<ProductRowItem>>(products);
                    Pagination.UpdatePaginationStatus(response.Result.Headers);
                }
                NotifyOfPropertyChange(() => Pagination);
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        private void LoadDetailItem(int id)
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingInfoVis = Visibility.Visible;
                var request = new RestRequest($"products/{id}", Method.Get);
                var respone = _managementRestClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var productDetails = respone.Result.Content;
                    ProductDetails = JsonConvert.DeserializeObject<ProductDetail>(productDetails);
                }
                LoadingInfoVis = Visibility.Hidden;
            })).Start();
        }

        #endregion

        #region Events
        public void Expander_Expanded()
        {
            if (SelectedProduct != null)
                LoadDetailItem(SelectedProduct.Id);
        }
        public void RowItemProducts_SelectionChanged()
        {

        }
        #endregion

        #region Pagination
        public void LoadFirstPage()
        {
            Pagination.LoadFirstPage();
        }
        public void LoadNextPage()
        {
            Pagination.LoadNextPage();
        }
        public void LoadLastPage()
        {
            Pagination.LoadLastPage();
        }
        #endregion

        #region View Mapping Properties
        public ProductView View => (ProductView)this.GetView();
        public DataGrid Grid => View.RowItemProducts;
        public Expander Expander => View.DetailExpander;
        public Snackbar GreenSB => View.GreenSB;
        public Snackbar RedSB => View.RedSB;
        #endregion

        #region Binding Properties
        public BindableCollection<ProductRowItem> RowItemProducts
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
        public ProductRowItem SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                if (value != null && Expander.IsExpanded)
                    LoadDetailItem(value.Id);
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
        public ProductDetail ProductDetails
        {
            get { return _productDetails; }
            set
            {
                _productDetails = value;
                NotifyOfPropertyChange(() => ProductDetails);
            }
        }
        #endregion


    }
}
