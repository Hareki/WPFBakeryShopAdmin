﻿using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Threading;
using System.Windows;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class DashboardViewModel : Screen
    {
        private readonly RestClient _restClient;
        private Visibility _loadingPageVis = Visibility.Visible;
        private Dashboard _dashboard;
        private float _cancelledPercent;
        private float _shippedPercent;
        private float _pendingPercent;
        private float _shippingPercent;

        #region Base
        public DashboardViewModel() : base()
        {
            this._restClient = RestConnection.ManagementRestClient;
            LoadPage();
        }
        private void UpdatePercent()
        {
            float todayOrdersNum = Dashboard.TodayOrdersNum;
            CancelledPercent = ((float)Dashboard.TodayCancelOrdersNum / todayOrdersNum) * 100;
            ShippingPercent = ((float)Dashboard.TodayDispatchOrdersNum / todayOrdersNum) * 100;
            ShippedPercent = ((float)Dashboard.TodayShippedOrdersNum / todayOrdersNum) * 100;
            PendingPercent = ((float)Dashboard.TodayProcessingOrdersNum / todayOrdersNum) * 100;
        }
        public void LoadPage()
        {
            new Thread(new ThreadStart(() =>
            {
                LoadingPageVis = Visibility.Visible;
                var request = new RestRequest("home", Method.Get);
                var respone = _restClient.ExecuteAsync(request);
                if ((int)respone.Result.StatusCode == 200)
                {
                    var dashboardContent = respone.Result.Content;
                    Dashboard = JsonConvert.DeserializeObject<Dashboard>(dashboardContent);
                    UpdatePercent();
                }
                LoadingPageVis = Visibility.Hidden;
            })).Start();

        }
        #endregion

        #region Binding Properties
        public float CancelledPercent
        {
            get { return _cancelledPercent; }
            set
            {
                _cancelledPercent = value;
                NotifyOfPropertyChange(() => CancelledPercent);
            }
        }
        public float PendingPercent
        {
            get { return _pendingPercent; }
            set
            {
                _pendingPercent = value;
                NotifyOfPropertyChange(() => PendingPercent);
            }
        }
        public float ShippingPercent
        {
            get { return _shippingPercent; }
            set
            {
                _shippingPercent = value;
                NotifyOfPropertyChange(() => ShippingPercent);
            }
        }
        public float ShippedPercent
        {
            get { return _shippedPercent; }
            set
            {
                _shippedPercent = value;
                NotifyOfPropertyChange(() => ShippedPercent);
            }
        }
        public Dashboard Dashboard
        {
            get { return _dashboard; }
            set
            {
                _dashboard = value;
                NotifyOfPropertyChange(() => Dashboard);
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
        #endregion

    }
}
