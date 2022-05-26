using System.Collections.Generic;
using WPFBakeryShopAdmin.Models;

namespace WPFBakeryShopAdmin.Utilities
{
    public class RoleList
    {
        public static List<ItemRole> LIST;
        static RoleList()
        {
            LIST = new List<ItemRole>();
            LIST.Add(new ItemRole(AuthRole.ROLE_ADMIN, "Quản trị viên"));
            LIST.Add(new ItemRole(AuthRole.ROLE_USER, "Người dùng"));
        }
    }
}
