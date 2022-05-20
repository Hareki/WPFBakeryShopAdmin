using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Utilities
{
    public class StringUtils
    {
        public static string FormatCurrency(int currency)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:C0}", currency);
        }

        public static string SerializeObject(object obj)
        {
            return JsonConvert.SerializeObject(obj, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });
        }

        private static readonly Regex _emailRegex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        public static bool IsValidEmail(string email)
        {
            return _emailRegex.IsMatch(email);
        }

        private static readonly Regex _phoneRegex = new Regex(@"^0[0-9]{9}$");
        public static bool IsValidPhoneNumber(string phoneNum)
        {
            return _phoneRegex.IsMatch(phoneNum);
        }
    }
}
