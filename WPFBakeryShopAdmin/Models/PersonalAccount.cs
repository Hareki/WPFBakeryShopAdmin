using System;
using System.Collections.Generic;
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
    }
}
