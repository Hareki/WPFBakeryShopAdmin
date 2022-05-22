using Caliburn.Micro;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        private List<ItemLanguage> _languageList;

        DashboardViewModel _dashboardViewModel;
        AccountViewModel _accountViewModel;
        BillViewModel _billViewModel;
        ProductViewModel _productViewModel;
        PersonalAccountViewModel _personalAccountViewModel;

        PersonalAccount _account;
        RestClient _restClient = RestConnection.AccountRestClient;

        #region Base
        public MainViewModel(DashboardViewModel dashboardViewModel, AccountViewModel accountViewModel, BillViewModel billViewModel, ProductViewModel productViewModel, PersonalAccountViewModel personalAccountViewModel)
        {
            _dashboardViewModel = dashboardViewModel;
            _accountViewModel = accountViewModel;
            _billViewModel = billViewModel;
            _productViewModel = productViewModel;
            _personalAccountViewModel = personalAccountViewModel;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LanguageList = Utilities.LanguageList.LIST;
            return Task.CompletedTask;
        }
        protected override void OnViewReady(object view)
        {
            ActivateItemAsync(_accountViewModel);
        }
        #endregion

        public void RefreshAccount()
        {
            new Thread(new ThreadStart(() =>
            {

            })).Start();
        }

        #region Loading Pages
        public void LoadDashboard()
        {
            if (ActiveItem != _dashboardViewModel)
            {
                ActivateItemAsync(_dashboardViewModel);
            }

        }
        public void LoadAccount()
        {
            if (ActiveItem != _accountViewModel)
            {
                ActivateItemAsync(_accountViewModel);
            }

        }
        public void LoadBill()
        {
            if (ActiveItem != _billViewModel)
            {
                ActivateItemAsync(_billViewModel);
            }

        }
        public void LoadProduct()
        {
            if (ActiveItem != _productViewModel)
            {
                ActivateItemAsync(_productViewModel);
            }

        }
        public void LoadPersonalAccount()
        {
            if (ActiveItem != _personalAccountViewModel)
            {
                ActivateItemAsync(_personalAccountViewModel);
            }
        }
        #endregion

        #region Properties
        public List<ItemLanguage> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                NotifyOfPropertyChange(() => LanguageList);
            }
        }
        #endregion
    }
}
