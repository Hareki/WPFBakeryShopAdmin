using Caliburn.Micro;
using Newtonsoft.Json;
using RestSharp;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class MainViewModel : Conductor<Screen>.Collection.OneActive, IHandle<PersonalAccount>
    {
        private List<ItemLanguage> _languageList;

        private DashboardViewModel _dashboardViewModel;
        private AccountViewModel _accountViewModel;
        private BillViewModel _billViewModel;
        private ProductViewModel _productViewModel;
        private PersonalAccountViewModel _personalAccountViewModel;
        private IEventAggregator _eventAggregator;

        private PersonalAccount _personalAccount;
        private RestClient _restClient;

        #region Base
        public MainViewModel(DashboardViewModel dashboardViewModel, AccountViewModel accountViewModel,
            BillViewModel billViewModel, ProductViewModel productViewModel,
            PersonalAccountViewModel personalAccountViewModel, IEventAggregator eventAggregator)
        {
            _dashboardViewModel = dashboardViewModel;
            _accountViewModel = accountViewModel;
            _billViewModel = billViewModel;
            _productViewModel = productViewModel;
            _personalAccountViewModel = personalAccountViewModel;

            _eventAggregator = eventAggregator;
            Items.AddRange(new Screen[] { _dashboardViewModel, _accountViewModel, _billViewModel, _productViewModel, _personalAccountViewModel });
        }
        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            LanguageList = Utilities.LanguageList.LIST;
            _restClient = RestConnection.AccountRestClient;
            _eventAggregator.SubscribeOnPublishedThread(this);
            return Task.CompletedTask;
        }
        protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
        {
            _eventAggregator.Unsubscribe(this);
            return Task.CompletedTask;
        }
        protected override void OnViewReady(object view)
        {
            RefreshAccountInfo();
            ActivateItemAsync(_accountViewModel);
        }
        public void RefreshAccountInfo()
        {
            new Thread(new ThreadStart(() =>
            {
                var response = RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "", null, null);
                if ((int)response.Result.StatusCode == 200)
                {
                    var personalAccount = response.Result.Content;
                    PersonalAccount = JsonConvert.DeserializeObject<PersonalAccount>(personalAccount);
                }
            })).Start();
        }
        #endregion

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
                // _personalAccountViewModel = new PersonalAccountViewModel(null);
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
        public PersonalAccount PersonalAccount
        {
            get
            { return _personalAccount; }
            set
            {
                _personalAccount = value;
                //      _eventAggregator.PublishOnUIThreadAsync(_personalAccount);
                NotifyOfPropertyChange(() => PersonalAccount);
            }
        }
        #endregion

        #region Singleton handler
        public Task HandleAsync(PersonalAccount account, CancellationToken cancellationToken)
        {
            PersonalAccount = account;
            return Task.CompletedTask;
        }
        #endregion
    }
}
