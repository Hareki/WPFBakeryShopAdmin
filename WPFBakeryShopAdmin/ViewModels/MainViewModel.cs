using Caliburn.Micro;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class MainViewModel : Conductor<object>
    {
        public MainViewModel() : base()
        {
            ActivateItemAsync(new DashboardViewModel());
        }
        public void LoadDashboard()
        {
            ActivateItemAsync(new DashboardViewModel());
        }
        public void LoadAccount()
        {
            ActivateItemAsync(new AccountViewModel());
        }
        public void LoadBill()
        {
            ActivateItemAsync(new BillViewModel());
        }
        public void LoadProduct()
        {
            ActivateItemAsync(new ProductViewModel());
        }
        public void LoadPersonalAccount()
        {
            ActivateItemAsync(new PersonalAccountViewModel());
        }
    }
}
