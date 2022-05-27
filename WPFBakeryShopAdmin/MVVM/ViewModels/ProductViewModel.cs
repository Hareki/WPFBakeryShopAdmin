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
using WPFBakeryShopAdmin.MVVM.Models;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ProductViewModel : Screen, IViewModel
    {
        private RestClient _managementRestClient = RestConnection.ManagementRestClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingProductInfoVis = Visibility.Hidden;
        private Visibility _loadingVariantVis = Visibility.Hidden;
        private Visibility _loadingProductImages = Visibility.Hidden;
        private BindableCollection<ProductRowItem> _rowItemProducts;
        private BindableCollection<Variant> _rowItemVariants;
        private BindableCollection<ProductImage> _rowItemImages;
        private ProductRowItem _selectedProduct;
        private Variant _selectedVariant;
        private ProductImage _selectedImage;
        private ProductDetail _productDetails;
        private Pagination _pagination;
        private IWindowManager _windowManager;
        private BindableCollection<Category> _categoryList;
        private BindableCollection<ProductType> _typeList;
        private Category _selectedCategory;

        #region Base
        public ProductViewModel(IWindowManager windowManager)
        {
            _windowManager = windowManager;
            Pagination = new Pagination(10, this);
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _managementRestClient = RestConnection.ManagementRestClient;
            _ = LoadPageAsync();
            return Task.CompletedTask;
        }
        public Task LoadPageAsync()
        {
            if (RowItemProducts != null)
                RowItemProducts.Clear();

            LoadingPageVis = Visibility.Visible;

            _ = LoadCategoryList();
            _ = LoadTypeList();

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
            return Task.CompletedTask;
        }

        private async Task LoadTypeList()
        {
            var typeList = Lists.TypeList.LoadTypeList();
            TypeList = new BindableCollection<ProductType>((await typeList));
        }

        private async Task LoadCategoryList()
        {
            var categoryList = Lists.CategoryList.LoadCategoryList();
            CategoryList = new BindableCollection<Category>((await categoryList));
        }
        private Task LoadDetailItemAsync(int id)
        {
            LoadingInfoVis = Visibility.Visible;
            LoadingVariantVis = Visibility.Visible;
            LoadingProductImages = Visibility.Visible;

            _ = LoadProductInformation(id);
            _ = LoadVariants(id);
            _ = LoadProductImages(id);

            return Task.CompletedTask;
        }

        private async Task LoadProductInformation(int id)
        {
            var request = new RestRequest($"products/{id}", Method.Get);
            var respone = await _managementRestClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var productDetails = respone.Content;
                ProductDetails = JsonConvert.DeserializeObject<ProductDetail>(productDetails);
            }
            LoadingInfoVis = Visibility.Hidden;
        }
        private async Task LoadVariants(int id)
        {
            var request = new RestRequest($"products/variants/{id}", Method.Get);
            var respone = await _managementRestClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var variants = respone.Content;
                RowItemVariants = JsonConvert.DeserializeObject<BindableCollection<Variant>>(variants);
            }
            LoadingVariantVis = Visibility.Hidden;
        }
        private async Task LoadProductImages(int id)
        {
            var request = new RestRequest($"products/{id}/images", Method.Get);
            var respone = await _managementRestClient.ExecuteAsync(request);
            if ((int)respone.StatusCode == 200)
            {
                var productImages = respone.Content;
                RowItemImages = JsonConvert.DeserializeObject<BindableCollection<ProductImage>>(productImages);
            }
            LoadingProductImages = Visibility.Hidden;
        }
        #endregion

        #region Events
        public void Expander_Expanded()
        {
            if (SelectedProduct != null)
                _ = LoadDetailItemAsync(SelectedProduct.Id);
        }
        public void RowItemProducts_SelectionChanged()
        {
            View.Dispatcher.Invoke(() =>
            {
                if (Grid.SelectedIndex < 0)
                {
                    ProductDetails = null;
                }
            });
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
        public ComboBox CBCategoryList => View.CategoryList;
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
        public BindableCollection<Variant> RowItemVariants
        {
            get => _rowItemVariants;
            set
            {
                _rowItemVariants = value;
                NotifyOfPropertyChange(() => RowItemVariants);
            }
        }
        public BindableCollection<ProductImage> RowItemImages
        {
            get => _rowItemImages;
            set
            {
                _rowItemImages = value;
                NotifyOfPropertyChange(() => RowItemImages);
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
            get { return _loadingProductInfoVis; }
            set
            {
                _loadingProductInfoVis = value;
                NotifyOfPropertyChange(() => LoadingInfoVis);
            }
        }
        public Visibility LoadingVariantVis
        {
            get => _loadingVariantVis;
            set
            {
                _loadingVariantVis = value;
                NotifyOfPropertyChange(() => LoadingVariantVis);
            }
        }
        public Visibility LoadingProductImages
        {
            get => _loadingProductImages;
            set
            {
                _loadingProductImages = value;
                NotifyOfPropertyChange(() => LoadingProductImages);
            }
        }
        public ProductRowItem SelectedProduct
        {
            get { return _selectedProduct; }
            set
            {
                _selectedProduct = value;
                if (value != null && Expander.IsExpanded)
                    _ = LoadDetailItemAsync(value.Id);
                NotifyOfPropertyChange(() => SelectedProduct);
            }
        }
        public Variant SelectedVariant
        {
            get => _selectedVariant;
            set
            {
                _selectedVariant = value;
                NotifyOfPropertyChange(() => SelectedVariant);
            }
        }
        public ProductImage SelectedImage
        {
            get => _selectedImage;
            set
            {
                _selectedImage = value;
                NotifyOfPropertyChange(() => SelectedImage);
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
                if (value != null)
                {
                    SelectedCategory = value.Category;
                }
                NotifyOfPropertyChange(() => ProductDetails);
            }
        }
        public BindableCollection<Category> CategoryList
        {
            get => _categoryList;
            set
            {
                _categoryList = value;
                NotifyOfPropertyChange(() => CategoryList);
            }
        }
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                _selectedCategory = value;
                NotifyOfPropertyChange(() => SelectedCategory);
            }
        }
        public BindableCollection<ProductType> TypeList
        {
            get => _typeList;
            set
            {
                _typeList = value;
                NotifyOfPropertyChange(() => TypeList);
            }
        }
        #endregion



    }
}
