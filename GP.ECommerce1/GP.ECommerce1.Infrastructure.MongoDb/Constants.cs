namespace GP.ECommerce1.Infrastructure.MongoDb;

public class Constants
{
    public const string DatabaseName = "GP_ECommerce1";
    public const string ConnectionString = "mongodb://localhost:27017";

    public static string GetDatabaseName()
    {
        var name = Environment.GetEnvironmentVariable("DB_NAME");
        if (name == null)
            throw new Exception("Database name was not found in the env variables.");
        return name;
    }
    
    public const string CategoriesCollectionName = "Categories";
    public const string DiscountsCollectionName = "Disounts";
    public const string ProductsCollectionName = "Products";
    public const string CustomersCollectionName = "Customers";
    public const string ReviewsCollectionName = "Reviews";
    public const string ShoppingCartsCollectionName = "Carts";
    public const string WishListsCollectionName = "WishLists";
    public const string OrdersCollectionName = "Orders";
}