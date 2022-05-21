using System.Collections.Generic;
using WPFBakeryShopAdmin.Models;

namespace WPFBakeryShopAdmin.Utilities
{
    public class LanguageList
    {
        public static List<ItemLanguage> LIST;
        static LanguageList()
        {
            LIST = new List<ItemLanguage>();
            LIST.Add(new ItemLanguage("Tiếng Việt", "/Images/vn-flag.png", "vi"));
            LIST.Add(new ItemLanguage("English", "/Images/uk-flag.png", "en"));
        }
    }
}
