using System.Globalization;
using System.Windows.Controls;
using WPFBakeryShopAdmin.Utilities;

namespace WPFBakeryShopAdmin.LocalValidationRules
{
    public class EmailFormatValidationRule : ValidationRule
    {
        public string Message { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (StringUtils.IsValidEmail(value?.ToString()))
            {
                return ValidationResult.ValidResult;
            }
            return new ValidationResult(false, Message ?? "Email is not valid");
        }
    }
}
