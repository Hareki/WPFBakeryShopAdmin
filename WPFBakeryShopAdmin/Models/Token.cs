using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
