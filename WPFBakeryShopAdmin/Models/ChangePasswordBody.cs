using Newtonsoft.Json;

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
