using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFBakeryShopAdmin.Models
{
    public class DetailItemProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Ingredients { get; set; }
        public string Allergens { get; set; }
        public bool Available { get; set; }
    }
}
