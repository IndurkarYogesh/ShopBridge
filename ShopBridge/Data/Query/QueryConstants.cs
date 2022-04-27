namespace ShopBridge.QueryConstants
{
    internal class Constants
    {
        internal const string GetAllProducts = "SELECT * FROM PRODUCT";
        internal const string GetAllProductProperties = "SELECT * FROM PRODUCTPROPERTIES";
        internal const string GetAllCategory = "SELECT * FROM CATEGORY";
        internal const string GetProductById = "SELECT * FROM PRODUCT WHERE PRODUCTID=@PRODUCTID";
        internal const string GetProductByProductName = "SELECT * FROM PRODUCT WHERE PRODUCTNAME=@PRODUCTNAME";
        internal const string GetProductPropertiesByProductId = "SELECT * FROM PRODUCTPROPERTYMAPPING WHERE PRODUCTID=@PRODUCTID";
        internal const string GetProductCategoryByProductId = "SELECT * FROM PRODUCTCATEGORYMAPPING WHERE PRODUCTID=@PRODUCTID";
        internal const string DeleteProductById = "DELETE FROM PRODUCT WHERE PRODUCTID=@PRODUCTID";
        internal const string InsertNewProduct = "INSERT INTO PRODUCT(PRODUCTNAME) VALUES(@PRODUCTNAME)";
        internal const string InsertProductPropertiesMapping = "INSERT INTO PRODUCTPROPERTYMAPPING(PRODUCTID,PROPERTYID,PROPERTYVALUE) VALUES(@PRODUCTID,@PROPERTYID,@PROPERTYVALUE)";
        internal const string InsertProductCategoryMapping = "INSERT INTO PRODUCTCATEGORYMAPPING(PRODUCTID,CATEGORYID) VALUES(@PRODUCTID,@CATEGORYID)";

        internal const string UpdateProduct = "UPDATE PRODUCT SET PRODUCTNAME=@PRODUCTNAME WHERE PRODUCTID=@PRODUCTID";
        internal const string UpdateProductPropertiesMapping = "UPDATE PRODUCTPROPERTYMAPPING SET PROPERTYVALUE=@PROPERTYVALUE WHERE PRODUCTID=@PRODUCTID AND PROPERTYID=@PROPERTYID";
        //internal const string UpdateProductCategoryMapping = "UPDATE PRODUCTCATEGORYMAPPING SET CATEGORYID=@CATEGORYID WHERE PRODUCTID=@PRODUCTID AND CATEGORYID=@OLDCATEGORYID";
    }
}