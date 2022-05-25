using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Models
{
    public class ChangePasswordBody
    {
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        [JsonIgnore]
        public string ConfirmNewPassword { get; set; }
    }
}
