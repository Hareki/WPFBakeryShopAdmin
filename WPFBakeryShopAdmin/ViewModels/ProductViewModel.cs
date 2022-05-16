using Caliburn.Micro;
using RestSharp;
using WPFBakeryShopAdmin.Utilities;
using WPFBakeryShopAdmin.Models;
using Newtonsoft.Json;
using RestSharp.Authenticators;

namespace WPFBakeryShopAdmin.ViewModels
{
    public class ProductViewModel : Conductor<object>
    {
        private RestClient RestClient;
        public BindableCollection<RowItemProduct> RowItemProducts { get; set; }
        public ProductViewModel() : base()
        {
            LoadEditingProduct();
            this.RestClient = new RestClient(RestConnection.ADMIN_BASE_CONNECTION_STRING);
            this.RestClient.Authenticator = new JwtAuthenticator("eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJhZG1pbkBnbWFpbC5jb20iLCJhdXRoIjoiUk9MRV9BRE1JTixST0xFX1VTRVIiLCJleHAiOjE2NTI1Nzg4MTF9.jSaNsyur6LQeB5vSi0sfx6buKqBxA00loZYC0kFz8sWdxcOqeDa63VRlP0MLCBl7rXAbPBXu_XQttaOQFhNT3A");
            LoadRowItemProducts();
        }
        public void LoadEditingProduct()
        {
            ActivateItemAsync(new EditingProductViewModel());
        }
        public void LoadAddingProduct()
        {
            ActivateItemAsync(new AddingProductViewModel());
        }

        public void LoadRowItemProducts()
        {
            var request = new RestRequest("products", Method.Get);
            request.AddParameter("page", 0).AddParameter("size", 100);
            var respone = RestClient.ExecuteAsync(request);
            if((int)respone.Result.StatusCode == 200)
            {
                var products = respone.Result.Content;
                RowItemProducts = JsonConvert.DeserializeObject<BindableCollection<RowItemProduct>>(products);   
            }

        }
    }
}
