using Shared.Models.DataBaseModels.Account;
using Shared.Models.DataBaseModels.Costumers;
using Shared.Models.DataBaseModels.Inventory;
using Shared.Models.DataBaseModels.Sales;

namespace DataBaseManager.MockData;

public static class MockData
{
    public static List<Sales> MockSalesData = new()
    {

    };
    public static List<Inventory> MockInventoryData = new()
    {

    };
    public static List<Product> MockProductData = new()
    {
        new Carpet
        {

        },
        new RawMaterial
        {

        },
        new Rug
        {

        }
    };
    public static List<Account> MockAccountData = new()
    {

    };
    public static List<Customer> MockCustomerData = new()
    {


    };
}
