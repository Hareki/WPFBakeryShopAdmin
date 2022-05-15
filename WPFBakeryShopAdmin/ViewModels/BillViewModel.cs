using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class BillViewModel : Screen
    {
        private readonly RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private BindableCollection<RowItemBill> _rowItemBills;
        private BindingButtonAppearance _bindingButton;
        private RowItemBill _selectedBill;
        private DetailItemBill _billDetails;
        private readonly int _pageSize = 11;
        private int _currentPage = 0;
        private int _maxPage;
        private bool _couldLoadFirstPage = false, _couldLoadPreviousPage = false, _couldLoadNextPage = false, _couldLoadLastPage = false;
        private string _pendingColor = "#0DA012";
        private string _shippingColor = "#4C82AF";
        private string _shippedColor = "#AF4C8E";
        private string _cancelledColor = "#E63946";



        public BillViewModel() : base()
        {
            this._restClient = RestConnection.REST_CLIENT;
            LoadPage();
        }



        private void LoadDetailItem(int id)
        {
            new Thread(new ThreadStart(() =>
            {
                var request = new RestRequest($"orders/{id}", Method.Get);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var billDetails = respone.Result.Content;
                    BillDetails = JsonConvert.DeserializeObject<DetailItemBill>(billDetails);
                }
            })).Start();
        }

        private void SetBindingButtonAppearance(int statusId)
        {
            string text, imageUrl, background;
            switch (statusId)
            {
                case 1:
                    text = "Chờ duyệt";
                    background = _pendingColor;
                    imageUrl = "/Images/pending-white.png";
                    break;
                case 2:
                    text = "Đang giao";
                    background = _shippingColor;
                    imageUrl = "/Images/shipping-white.png";
                    break;
                case 3:
                    text = "Đã giao";
                    background = _shippedColor;
                    imageUrl = "/Images/shipped-white.png";
                    break;
                case 4:
                    text = "Đã hủy";
                    background = _cancelledColor;
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
        private void UpdatePagingStatus(IReadOnlyCollection<RestSharp.HeaderParameter> headers)
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

        public void UpdateOrderStatus()
        {
            new Thread(new ThreadStart(() =>
            {
                var request = new RestRequest($"orders/{BillDetails.Id}/status/update", Method.Put);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    BillDetails.StatusId++;
                }
            })).Start();
        }
        private void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                if (RowItemBills != null)
                {
                    RowItemBills.Clear();
                }
                LoadingPageVis = Visibility.Visible;
                var request = new RestRequest("orders", Method.Get);
                request.AddParameter("page", _currentPage).AddParameter("size", _pageSize);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var bills = respone.Result.Content;
                    RowItemBills = JsonConvert.DeserializeObject<BindableCollection<RowItemBill>>(bills);
                    UpdatePagingStatus(respone.Result.Headers);
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
            _currentPage = _maxPage;
            LoadPage();
        }
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
                if (value != null)
                    LoadDetailItem(value.Id);

                NotifyOfPropertyChange(() => SelectedBill);
            }
        }
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
