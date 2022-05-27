using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.MVVM.Views;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.MVVM.ViewModels
{
    public class AddingProductViewModel : Screen
    {
        private RestClient _restClient;
        private BindableCollection<Category> _categoryList;
        private Category _selectedCategory;
        private ProductDetail _productDetails;
        #region Base
        protected override async Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            ProductDetails = new ProductDetail();
            await LoadCategoryList();

        }
        private async Task LoadCategoryList()
        {
            var categoryList = Lists.CategoryList.LoadCategoryList();
            CategoryList = new BindableCollection<Category>((await categoryList));
            if (CategoryList != null && CategoryList.Count > 0)
            {
                ProductDetails.CategoryId = CategoryList[0].Id;
                SelectedCategory = CategoryList[0];
            }
        }

        public async Task AddNewProduct()
        {
            if (SelectedCategory == null)
            {
                await DialogHost.Show(View.DialogContent);
                CancelAdding();
                return;
            }
            string JSonAccountInfo = StringUtils.SerializeObject(ProductDetails);
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Post, "products", JSonAccountInfo, "application/json");
            int statusCode = (int)response.StatusCode;
            if (statusCode == 201)
            {
                ResetFields();
                ShowSuccessMessage("Thêm sản phẩm thành công");
            }
            else if (statusCode == 400)
            {
                ShowFailMessage("Tên sản phẩm đã tồn tại");
            }
            else if (statusCode == 404)
            {
                ShowFailMessage("Danh mục không còn tồn tại, vui lòng tải lại trang");
            }
        }

        private void ResetFields()
        {
            ProductDetails = new ProductDetail();
            View.Dispatcher.Invoke(() =>
            {
                View.CategoryList.SelectedIndex = 0;
            });

        }

        public void CancelAdding()
        {
            this.TryCloseAsync();
        }
        #endregion

        #region Binding Property
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
        #endregion

        #region Mapping Region
        public AddingProductView View => (AddingProductView)this.GetView();
        public Snackbar GreenSB => View.GreenSB;
        public Snackbar RedSB => View.RedSB;
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
    }
}
