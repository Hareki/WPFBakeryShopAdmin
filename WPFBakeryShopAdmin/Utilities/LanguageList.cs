using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFBakeryShopAdmin.Models;

namespace WPFBakeryShopAdmin.Utilities
{
    public class LanguageList
    {
        public static List<ItemLanguage> LIST;
        static LanguageList()
        {
            LIST = new List<ItemLanguage>();
            LIST.Add(new ItemLanguage("Tiếng Việt", "/Images/vn-flag.png"));
            LIST.Add(new ItemLanguage("English", "/Images/uk-flag.png"));
        }
    }
}
