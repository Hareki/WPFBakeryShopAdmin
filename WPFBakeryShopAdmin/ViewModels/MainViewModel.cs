using Caliburn.Micro;
using System.Collections.Generic;
using WPFBakeryShopAdmin.Models;

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

        #region Base
        public MainViewModel() : base()
        {
            ActivateItemAsync(new DashboardViewModel());
            LanguageList = Utilities.LanguageList.LIST;

        }
        #endregion

        #region Loading Pages
        public void LoadDashboard()
        {
            if (ActiveItem != _dashboardViewModel)
            {
                _dashboardViewModel = new DashboardViewModel();
                ActivateItemAsync(_dashboardViewModel);
            }

        }
        public void LoadAccount()
        {
            if (ActiveItem != _accountViewModel)
            {
                _accountViewModel = new AccountViewModel();
                ActivateItemAsync(_accountViewModel);
            }

        }
        public void LoadBill()
        {
            if (ActiveItem != _billViewModel)
            {
                _billViewModel = new BillViewModel();
                ActivateItemAsync(_billViewModel);
            }

        }
        public void LoadProduct()
        {
            if (ActiveItem != _productViewModel)
            {
                _productViewModel = new ProductViewModel();
                ActivateItemAsync(_productViewModel);
            }

        }
        public void LoadPersonalAccount()
        {
            if (ActiveItem != _personalAccountViewModel)
            {
                _personalAccountViewModel = new PersonalAccountViewModel();
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
