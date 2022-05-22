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
        private Visibility _loadingPageVis = Visibility.Visible;
        private Visibility _loadingInfoVis = Visibility.Visible;
        private BindableCollection<RowItemBill> _rowItemBills;
        private BindingButtonAppearance _bindingButton;
        private RowItemBill _selectedBill;
        private DetailItemBill _billDetails;
        private string _pageIndicator;
        private int _totalCount;
        private readonly int _pageSize = 10;
        private int _currentPage = 0;
        private int _maxPageIndex;
        private bool _couldLoadFirstPage = false, _couldLoadPreviousPage = false, _couldLoadNextPage = false, _couldLoadLastPage = false;


        #region Base
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            _restClient = RestConnection.ManagementRestClient;
            LoadPage(-1);
            return Task.CompletedTask;
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
        public void LoadPage(int page)
        {
            if (page != -1) _currentPage = page;
            new Thread(new ThreadStart(() =>
            {
                if (RowItemBills != null)
                    RowItemBills.Clear();

                LoadingPageVis = Visibility.Visible;

                if (_currentPage > _maxPageIndex) _currentPage = 0;
                var list = new List<KeyValuePair<string, string>>() {
                      new KeyValuePair<string, string>("page", _currentPage.ToString()),
                      new KeyValuePair<string, string>("size", _pageSize.ToString()),
                };
                var response = RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "orders", list);

                if ((int)response.Result.StatusCode == 200)
                {
                    var bills = response.Result.Content;
                    RowItemBills = JsonConvert.DeserializeObject<BindableCollection<RowItemBill>>(bills);
                    UpdatePageStatus(response.Result.Headers);
                }
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
                    ShowSuccessMessage("Cập nhật trạng thái đơn hàng thành công");
                    return;
                }
                ShowFailMessage("Xảy ra lỗi trong quá trình cập nhật");
            })).Start();
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
        public void PreviewStatus()
        {
            if (BillDetails.CanUpdateOrderStatus)
            {
                int previewId = BillDetails.StatusId + 1;
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
    public class BindingButtonAppearance : Screen
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
