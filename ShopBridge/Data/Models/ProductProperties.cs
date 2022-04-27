using System.Collections.Generic;
namespace ShopBridge.Data
{
    public static class AllProductProperties
    {
        public static List<ProductProperties> allProperties { get; set; }

        static AllProductProperties()
        {
            allProperties = new List<ProductProperties>();
        }
    }

    public static class AllCategories
    {
        public static List<Category> allCategories { get; set; }
        static AllCategories()
        {
            allCategories = new List<Category>();
        }
    }
    public class ProductProperties
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}