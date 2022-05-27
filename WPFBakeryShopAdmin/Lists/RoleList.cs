using System.Collections.Generic;
using WPFBakeryShopAdmin.Models;

namespace WPFBakeryShopAdmin.Lists
{
    public class RoleList
    {
        public static List<Role> LIST;
        static RoleList()
        {
            LIST = new List<Role>();
            LIST.Add(new Role(AuthRole.ROLE_ADMIN, "Quản trị viên"));
            LIST.Add(new Role(AuthRole.ROLE_USER, "Người dùng"));
        }
    }
}
