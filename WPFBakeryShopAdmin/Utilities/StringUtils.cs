﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Utilities
{
    public class StringUtils
    {
        public static string FormatCurrency(int currency)
        {
            return string.Format(new CultureInfo("vi-VN"), "{0:C0}", currency);
        }
    }
}