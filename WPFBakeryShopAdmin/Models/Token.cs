using Newtonsoft.Json;

namespace WPFBakeryShopAdmin.Models
{
    public class Token
    {
        public Token(string idToken)
        {
            IdToken = idToken;
        }

        [JsonProperty("id_token")]
        public string IdToken { get; set; }
    }
}
