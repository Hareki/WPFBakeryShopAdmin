using Caliburn.Micro;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ProductViewModel : Conductor<object>
    {
        public ProductViewModel() : base()
        {
            LoadEditingProduct();
        }
        public void LoadEditingProduct()
        {
            ActivateItemAsync(new EditingProductViewModel());
        }
        public void LoadAddingProduct()
        {
            ActivateItemAsync(new AddingProductViewModel());
        }
    }
}
