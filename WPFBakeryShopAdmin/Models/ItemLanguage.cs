using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Models
{
    public class ItemLanguage
    {
        public ItemLanguage(string languageName, string languageFlagURL)
        {
            LanguageFlagURL = languageFlagURL;
            LanguageName = languageName;
        }
        public string LanguageName { get; set; }
        public string LanguageFlagURL { get; set; }
    }
}
