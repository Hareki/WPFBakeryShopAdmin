using Newtonsoft.Json;

namespace WPFBakeryShopAdmin.Models
{
    public class ProductDetail
    {
        public ProductDetail()
        {
        }

        public ProductDetail(ProductDetail another)
        {
            Id = another.Id;
            Name = another.Name;
            CategoryId = another.CategoryId;
            Ingredients = another.Ingredients;
            Allergens = another.Allergens;
            Available = another.Available;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string Ingredients { get; set; }
        public string Allergens { get; set; }
        public bool Available { get; set; }
        [JsonIgnore]
        public Category Category => Lists.CategoryList.FindCategoryById(CategoryId);
    }
}
