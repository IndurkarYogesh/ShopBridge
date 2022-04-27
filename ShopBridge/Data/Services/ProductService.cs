using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using ShopBridge.Connection;
using ShopBridge.QueryConstants;

namespace ShopBridge.Data
{
    public class ProductService : IProductService
    {
        public List<ProductProperties> GetAllProductProperties()
        {
            try
            {
                if(AllProductProperties.allProperties.Count == 0)
                {
                    using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                    {
                        IDbCommand properties = ShopBridgeConnection.GetDBCommand(Constants.GetAllProductProperties, dbConnection);
                        using(var readerP = properties.ExecuteReader())
                        {
                            while(readerP.Read())
                            {
                                int propId = int.Parse(readerP["PROPERTYID"].ToString());
                                string propname = readerP["PROPERTYNAME"].ToString();
                                AllProductProperties.allProperties.Add(new ProductProperties(){
                                    id = propId,
                                    name = propname
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return AllProductProperties.allProperties;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                if(AllCategories.allCategories.Count == 0)
                {
                    using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                    {
                        IDbCommand properties = ShopBridgeConnection.GetDBCommand(Constants.GetAllCategory, dbConnection);
                        using(var readerP = properties.ExecuteReader())
                        {
                            while(readerP.Read())
                            {
                                int Id = int.Parse(readerP["CATEGORYID"].ToString());
                                string name = readerP["CATEGORYNAME"].ToString();
                                AllCategories.allCategories.Add(new Category(){
                                    id = Id,
                                    name = name
                                });
                            }
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            return AllCategories.allCategories;
        }

        public string AddProduct(Product newProduct)
        {
            string lstrMsg = "";
            try
            {
                using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                {
                    IDbCommand cmd = ShopBridgeConnection.GetDBCommand(Constants.InsertNewProduct, dbConnection);
                    IDbDataParameter lProductName = ShopBridgeConnection.GetDBParameter("@PRODUCTNAME", DbType.String);
                    lProductName.Value = newProduct.Name;
                    cmd.Parameters.Add(lProductName);
                    int lint = cmd.ExecuteNonQuery();
                    if(lint > 0)
                    {
                        IDbCommand lGetcmd = ShopBridgeConnection.GetDBCommand(Constants.GetProductByProductName, dbConnection);
                        IDbDataParameter name = ShopBridgeConnection.GetDBParameter("@PRODUCTNAME", DbType.String);
                        name.Value = newProduct.Name;
                        lGetcmd.Parameters.Add(name);   
                        int Id = -1;
                        using(var reader = lGetcmd.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                               Id  = int.Parse(reader["ProductId"].ToString());
                            }
                        }

                        List<ProductProperties> llproperties = GetAllProductProperties();
                        foreach(ProductProperties prop in  llproperties)
                        {
                            IDbCommand prodProp = ShopBridgeConnection.GetDBCommand(Constants.InsertProductPropertiesMapping, dbConnection);
                            IDbDataParameter lProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int16);
                            lProductId.Value = Id;
                            prodProp.Parameters.Add(lProductId);
                            IDbDataParameter lpropId = ShopBridgeConnection.GetDBParameter("@PROPERTYID", DbType.Int16);
                            lpropId.Value = prop.id;
                            prodProp.Parameters.Add(lpropId);
                            IDbDataParameter propval = ShopBridgeConnection.GetDBParameter("@PROPERTYVALUE", DbType.String);
                            switch(prop.name)
                            {
                                case "Description":
                                    propval.Value = newProduct.Description;
                                    break;
                                case "Price":
                                    propval.Value = newProduct.Price;
                                    break;
                            }
                            prodProp.Parameters.Add(propval);

                            var lrprop = prodProp.ExecuteNonQuery();
                            
                        }

                        List<Category> llstcat = GetAllCategories();

                        Category lcat = llstcat.FirstOrDefault(cat=>cat.name == newProduct.Category);
                        if(lcat != null)
                        {
                            IDbCommand lcato = ShopBridgeConnection.GetDBCommand(Constants.InsertProductCategoryMapping, dbConnection);
                            IDbDataParameter lProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int16);
                            lProductId.Value = Id;
                            lcato.Parameters.Add(lProductId);
                            IDbDataParameter catId = ShopBridgeConnection.GetDBParameter("@CATEGORYID", DbType.Int16);
                            catId.Value = lcat.id;
                            lcato.Parameters.Add(catId);

                            var lrCat = lcato.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        throw new Exception("");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                lstrMsg = "Something went wrong while Adding new product.";
            }
            return lstrMsg;
        }

        public void DeleteProduct(int id)
        {
            try
            {
                using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                {
                    IDbCommand cmd = ShopBridgeConnection.GetDBCommand(Constants.DeleteProductById, dbConnection);
                    IDbDataParameter lProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int16);
                    lProductId.Value = id;
                    cmd.Parameters.Add(lProductId);
                    var res = cmd.ExecuteScalar();
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong while deleting product.");
            }
        }

        public List<Product> GetAllProducts()
        {
            List<Product> llstProducts = new List<Product>();
            try
            {
                using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                {
                    IDbCommand cmd = ShopBridgeConnection.GetDBCommand(Constants.GetAllProducts, dbConnection);
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            llstProducts.Add(new Product(){
                                Id = int.Parse(reader["ProductId"].ToString()),
                                Name = reader["ProductName"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong while getting all products.");
            }
            return llstProducts;
        }

        public Product GetProductById(int id)
        {
            Product lProduct = null;
             try
            {
                using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                {
                    IDbCommand cmd = ShopBridgeConnection.GetDBCommand(Constants.GetProductById, dbConnection);
                    IDbDataParameter lProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int32);
                    lProductId.Value = id;
                    cmd.Parameters.Add(lProductId);
                    using(var reader = cmd.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            lProduct = new Product(){
                                Id = int.Parse(reader["ProductId"].ToString()),
                                Name = reader["ProductName"].ToString()
                            };
                        }
                    }

                    if(lProduct != null)
                    {
                        IDbCommand lprodprop = ShopBridgeConnection.GetDBCommand(Constants.GetProductPropertiesByProductId, dbConnection);
                        IDbDataParameter lPProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int32);
                        lPProductId.Value = id;
                        lprodprop.Parameters.Add(lPProductId);
                        using(var readerP = lprodprop.ExecuteReader())
                        {
                            List<ProductProperties> llstProperties = GetAllProductProperties();
                            while(readerP.Read())
                            {
                                string propval = readerP["PROPERTYVALUE"].ToString();
                                ProductProperties lprop = llstProperties.FirstOrDefault(prop=>prop.id == int.Parse(readerP["PROPERTYID"].ToString()));
                                if(lprop != null)
                                {
                                    switch(lprop.name)
                                    {
                                        case "Description":
                                        lProduct.Description = propval;
                                        break;
                                        case "Price":
                                        lProduct.Price = double.Parse(propval);
                                        break;
                                    }
                                }
                            }
                        }

                        IDbCommand lcatcmd = ShopBridgeConnection.GetDBCommand(Constants.GetProductCategoryByProductId, dbConnection);
                        IDbDataParameter lCProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int32);
                        lCProductId.Value = id;
                        lcatcmd.Parameters.Add(lCProductId);
                        using(var readerc = lcatcmd.ExecuteReader())
                        {
                            List<Category> llstCategory = GetAllCategories();
                            while(readerc.Read())
                            {
                                string catID = readerc["CATEGORYID"].ToString();
                                Category lcat = llstCategory.FirstOrDefault(cat=>cat.id == int.Parse(catID));
                                if(lcat != null)
                                {
                                    lProduct.Category = lcat.name;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong while getting product.");
            }
            return lProduct;
        }

        public void UpdateProduct(int id, Product newProduct)
        {
            try
            {
                using (IDbConnection dbConnection = ShopBridgeConnection.GetDBConnection())
                {
                    IDbCommand cmd = ShopBridgeConnection.GetDBCommand(Constants.UpdateProduct, dbConnection);
                    IDbDataParameter lProductName = ShopBridgeConnection.GetDBParameter("@PRODUCTNAME", DbType.String);
                    lProductName.Value = newProduct.Name;
                    cmd.Parameters.Add(lProductName);
                    IDbDataParameter PRODUCTID = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int32);
                    PRODUCTID.Value = id;
                    cmd.Parameters.Add(PRODUCTID);

                    var updatep = cmd.ExecuteScalar();

                    List<ProductProperties> llproperties = GetAllProductProperties();
                    foreach(ProductProperties prop in  llproperties)
                    {
                        IDbCommand prodProp = ShopBridgeConnection.GetDBCommand(Constants.UpdateProductPropertiesMapping, dbConnection);
                        IDbDataParameter lProductId = ShopBridgeConnection.GetDBParameter("@PRODUCTID", DbType.Int16);
                        lProductId.Value = id;
                        prodProp.Parameters.Add(lProductId);
                        IDbDataParameter lpropId = ShopBridgeConnection.GetDBParameter("@PROPERTYID", DbType.Int16);
                        lpropId.Value = prop.id;
                        prodProp.Parameters.Add(lpropId);
                        IDbDataParameter propval = ShopBridgeConnection.GetDBParameter("@PROPERTYVALUE", DbType.String);
                        switch(prop.name)
                        {
                            case "Description":
                                propval.Value = newProduct.Description;
                                break;
                            case "Price":
                                propval.Value = newProduct.Price;
                                break;
                        }
                        prodProp.Parameters.Add(propval);

                        var lrprop = prodProp.ExecuteScalar();
                        
                    }
                }
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
                throw new Exception("Something went wrong while updating product.");
            }
        }
    }
}