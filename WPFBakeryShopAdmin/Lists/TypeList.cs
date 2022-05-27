using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.MVVM.Models;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.Lists
{
    public class TypeList
    {
        static List<ProductType> _types = null;
        private static readonly RestClient _restClient;
        public static bool Loading { get; set; }
        static TypeList()
        {
            _restClient = RestConnection.PublicRestClient;
        }
        public static async Task<List<ProductType>> LoadTypeList()
        {
            Loading = true;
            var response = await RestConnection.ExecuteRequestAsync(_restClient, Method.Get, "product-types", null, null);

            if ((int)response.StatusCode == 200)
            {
                var types = response.Content;
                _types = JsonConvert.DeserializeObject<List<ProductType>>(types);
            }
            Loading = false;
            return _types;
        }
        public static string FindTypeNameById(int id)
        {
            if (_types != null)
            {
                string result = _types.Find((element) => element.Id == id).Name;
                return result;
            }
            return null;
        }
    }
}
