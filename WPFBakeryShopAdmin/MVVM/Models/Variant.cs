using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.MVVM.Models
{
    public class Variant
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public int Cost { get; set; }
        public int Price { get; set; }
        public bool Hot { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public string AvailableName
        {
            get
            {
                if (Available) return "Còn hàng";
                else return "Hết hàng";
            }
        }
        [JsonIgnore]
        public string TypeName => Lists.TypeList.FindTypeNameById(TypeId);
        public string FormattedCost => Utilities.StringUtils.FormatCurrency(Cost);
        public string FormattedPrice => Utilities.StringUtils.FormatCurrency(Price);
    }
}

