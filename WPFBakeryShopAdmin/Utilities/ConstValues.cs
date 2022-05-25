using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFBakeryShopAdmin.Utilities
{
    public class ConstValues
    {
        public static readonly string PENDING_COLOR = Application.Current.TryFindResource("PendingColor") as string;
        public static readonly string SHIPPING_COLOR = Application.Current.TryFindResource("ShippingColor") as string;
        public static readonly string SHIPPED_COLOR = Application.Current.TryFindResource("ShippedColor") as string;
        public static readonly string CANCELLED_COLOR = Application.Current.TryFindResource("CancelledColor") as string;
        public static readonly string ROLE_ADMIN = "ROLE_ADMIN";
        public static readonly string ROLE_USER = "ROLE_USER";
    }
}
