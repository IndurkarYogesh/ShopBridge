using System.Collections.Generic;

namespace ShopBridge.Data
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        List<Category> GetAllCategories();
        Product GetProductById(int id);
        void UpdateProduct(int id, Product newProduct);
        void DeleteProduct(int id);
        string AddProduct(Product newProduct);
    }
}