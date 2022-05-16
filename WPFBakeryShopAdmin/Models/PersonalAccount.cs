using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Models
{
    public class PersonalAccount
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string ImageUrl { get; set; }
        public string Address { get; set; }
        public string LangKey { get; set; }
        public bool Activated { get; set; }
        public List<string> Authorities { get; set; }

        public string LanguageName
        {
            get
            {
                switch (LangKey)
                {
                    case "en": return "English";
                    case "vi": return "Việt Nam";
                    default: return "ERROR";
                }
            }
        }
        public int LanguageIndex
        {
            set
            {
                switch (value)
                {
                    case 0: LangKey = "vn"; break;
                    case 1: LangKey = "en"; break;
                    default: Debug.Assert(false); break;
                }
            }
            get
            {
                switch (LangKey)
                {
                    case "vi": return 0;
                    case "en": return 1;
                    default: return -1;
                }
            }
        }
    }
}
