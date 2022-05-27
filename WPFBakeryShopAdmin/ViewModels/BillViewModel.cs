using Caliburn.Micro;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Views;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class BillViewModel : Screen
    {
        private RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Hidden;
        private Visibility _loadingInfoVis = Visibility.Hidden;
        private BindableCollection<RowItemBill> _rowItemBills;
        private BindingButtonAppearance _bindingButton;
        private RowItemBill _selectedBill;
        private DetailItemBill _billDetails;
        private Pagination _pagination;
        #region Base
        public BillViewModel()
        {
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
                if (RowItemBills != null)
                    RowItemBills.Clear();

                LoadingPageVis = Visibility.Visible;
                var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", Pagination.CurrentPage.ToString()),
                      new KeyValuePair<string, string>("size", Pagination.PageSize.ToString()),
                };
                var response = RestConnection.ExecuteParameterRequestAsync(_restClient, Method.Get, "orders", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var bills = response.Result.Content;
                    RowItemBills = JsonConvert.DeserializeObject<BindableCollection<RowItemBill>>(bills);
                    Pagination.UpdatePaginationStatus(response.Result.Headers);
                }
                NotifyOfPropertyChange(() => Pagination);
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        private void LoadDetailItem(int id)
        {
            Console.WriteLine("Load Detail Item!");
            new Thread(new ThreadStart(() =>
            {
                LoadingInfoVis = Visibility.Visible;
                var request = new RestRequest($"orders/{id}", Method.Get);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var billDetails = respone.Result.Content;
                    BillDetails = JsonConvert.DeserializeObject<DetailItemBill>(billDetails);
                }
                LoadingInfoVis = Visibility.Hidden;
            })).Start();
        }
        public void UpdateOrderStatus()
        {
            new Thread(new ThreadStart(() =>
            {
                var request = new RestRequest($"orders/{BillDetails.Id}/status/update", Method.Put);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    BillDetails.StatusId++;
                    GridRefresh(BillDetails.StatusId);
                    BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                    NotifyOfPropertyChange(() => BillDetails);
                    ShowSuccessMessage("Cập nhật trạng thái đơn hàng thành công");
                    return;
                }
                ShowFailMessage("Xảy ra lỗi trong quá trình cập nhật");
            })).Start();
        }
        public async Task CancelOrder()
        {
            var result = await DialogHost.Show(View.DialogContent);
            bool confirm = System.Convert.ToBoolean(result);
            if (confirm)
            {
                var request = new RestRequest($"orders/{BillDetails.Id}/cancel", Method.Put);
                var respone = _restClient.ExecuteAsync(request);
                int statusCode = (int)respone.Result.StatusCode;
                if (statusCode == 200)
                {
                    BillDetails.StatusId = 4;
                    GridRefresh(BillDetails.StatusId);
                    BillDetails.CanCancel = false;//Hard code, do nếu muốn cái này thay đổi tự động theo logic của webservice thì phải load lại từ db
                    NotifyOfPropertyChange(() => BillDetails);
                    ShowSuccessMessage("Hủy đơn hàng thành công");
                    return;
                }
                else if (statusCode == 400)
                {
                    ShowFailMessage("Không thể hủy đơn hàng ở trạng thái này");
                    return;
                }
                ShowFailMessage("Xảy ra lỗi trong quá trình cập nhật");
            }
        }
        #endregion

        #region Updating View Methods
        private void GridRefresh(int newStatusId)
        {
            View.Dispatcher.Invoke(() =>
            {
                int selectedRow = Grid.SelectedIndex;

                RowItemBills[selectedRow].StatusId = newStatusId;
                Grid.Items.Refresh();
                Grid.SelectedIndex = selectedRow;
            });
        }
        public void PreviewUpdatedStatus()
        {
            if (BillDetails.CanUpdateOrderStatus)
            {
                int previewId = BillDetails.StatusId + 1;
                SetBindingButtonAppearance(previewId);
            }
        }
        public void PreviewCancelledStatus()
        {
            if (BillDetails.CanCancel)
            {
                int previewId = 4;
                SetBindingButtonAppearance(previewId);
            }
        }
        public void ClearPreview()
        {
            SetBindingButtonAppearance(BillDetails.StatusId);
        }
        private void SetBindingButtonAppearance(int statusId)
        {
            string text, imageUrl, background;
            switch (statusId)
            {
                case 1:
                    text = "Chờ duyệt";
                    background = ConstValues.PENDING_COLOR;
                    imageUrl = "/Images/pending-white.png";
                    break;
                case 2:
                    text = "Đang giao";
                    background = ConstValues.SHIPPING_COLOR;
                    imageUrl = "/Images/shipping-white.png";
                    break;
                case 3:
                    text = "Đã giao";
                    background = ConstValues.SHIPPED_COLOR;
                    imageUrl = "/Images/shipped-white.png";
                    break;
                case 4:
                    text = "Đã hủy";
                    background = ConstValues.CANCELLED_COLOR;
                    imageUrl = "/Images/cancelled-white.png";
                    break;
                default:
                    text = imageUrl = String.Empty;
                    background = "MediumSeaGrean";
                    break;
            }
            Console.WriteLine(text);
            Console.WriteLine(imageUrl);
            Console.WriteLine(background);
            BindingButton = new BindingButtonAppearance(text, imageUrl, background);
        }
        #endregion

        #region View Events
        public void RowItemBills_SelectionChanged()
        {

            View.Dispatcher.Invoke(() =>
            {
                if (Grid.SelectedIndex < 0)
                {
                    BillDetails = null;
                }
            });
        }
        public void Expander_Expanded()
        {
            if (SelectedBill != null)
                LoadDetailItem(SelectedBill.Id);
        }
        #endregion

        #region Showing Messages
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
                RedSB.Message.Content,
                null,
                null,
                null,
                false,
                true,
                TimeSpan.FromSeconds(3));
            });
        }
        #endregion

        #region Pagination
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
        public BindingButtonAppearance BindingButton
        {
            get { return _bindingButton; }
            set
            {
                _bindingButton = value;
                NotifyOfPropertyChange(() => BindingButton);
            }
        }
        public BindableCollection<RowItemBill> RowItemBills
        {
            get
            {
                return _rowItemBills;
            }
            set
            {
                _rowItemBills = value;
                NotifyOfPropertyChange(() => RowItemBills);
            }
        }
        public DetailItemBill BillDetails
        {
            get { return _billDetails; }
            set
            {
                _billDetails = value;
                if (value != null)
                    SetBindingButtonAppearance(BillDetails.StatusId);
                NotifyOfPropertyChange(() => BillDetails);
            }
        }
        public RowItemBill SelectedBill
        {
            get { return _selectedBill; }
            set
            {
                _selectedBill = value;
                if (value != null && Expander.IsExpanded)
                    LoadDetailItem(value.Id);

                NotifyOfPropertyChange(() => SelectedBill);
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

        #region View Mapping Properties
        public BillView View
        {
            get
            {
                return (BillView)this.GetView();
            }
        }
        public DataGrid Grid
        {
            get
            {
                return View.RowItemBills;
            }
        }
        public Expander Expander
        {
            get
            {
                return View.DetailExpander;
            }
        }
        public Snackbar GreenSB
        {
            get
            {
                return View.GreenSB;
            }
        }
        public Snackbar RedSB
        {
            get
            {
                return View.RedSB;
            }
        }
        #endregion
    }
    public class BindingButtonAppearance
    {
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string BackgroundColor { get; set; }
        public BindingButtonAppearance(string text, string imageUrl, string backgroundColor)
        {
            Text = text;
            ImageUrl = imageUrl;
            BackgroundColor = backgroundColor;
        }
    }
}
