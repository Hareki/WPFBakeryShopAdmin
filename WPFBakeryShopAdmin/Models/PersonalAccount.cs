using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;

namespace WPFBakeryShopAdmin.Models
{
    public class PersonalAccount
    {
        public PersonalAccount()
        {

        }
        public PersonalAccount(string firstName, string lastName, string email, string phone, List<string> authorities,
            string imageUrl, string address, string langKey, bool activated)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Phone = phone;
            this.Authorities = authorities;
            this.ImageUrl = imageUrl;
            this.Address = address;
            this.LangKey = langKey;
            this.Activated = activated;
        }

        public PersonalAccount Copy()
        {
            PersonalAccount account = new PersonalAccount(this.FirstName, this.LastName, this.Email, this.Phone, this.Authorities,
                this.ImageUrl, this.Address, this.LangKey, this.Activated);
            return account;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string LangKey { get; set; }

        public string ImageUrl { get; set; }
        public bool ShouldSerializeImageUrl() { return false; }
        public bool Activated { get; set; }
        public bool ShouldSerializeActivated() { return false; }
        public List<string> Authorities { get; set; }
        public bool ShouldSerializeAuthorities() { return false; }

        [JsonIgnore]
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

        [JsonIgnore]
        public int LanguageIndex
        {
            set
            {
                switch (value)
                {
                    case 0: LangKey = "vi"; break;
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
                    default: Debug.Assert(false); return -1;
                }
            }
        }

        [JsonIgnore]
        public string FullName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
    }
}
