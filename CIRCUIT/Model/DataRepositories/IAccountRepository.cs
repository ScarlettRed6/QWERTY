namespace CIRCUIT.Model.DataRepositories
{
    //For user account queries
    public interface IAccountRepository
    {
        bool InsertUser(UsersModel user);
        void UpdateUserAccount(UsersModel user);
        List<UsersModel> FetchUser();
        List<UsersModel> FetchUser(int userId);
        UsersModel FetchUserPassAndSalt(string username);
        void DeactivateUserAccount(List<int> userIds);
        void ActivateUserAccount(List<int> userIds);
    }

    //For sales and sale items queries
    public interface ISalesRepository
    {
        List<SaleModel> FetchSales();
        Dictionary<int, string> GetProductNames(List<int> productIds);
        List<SalesItemModel> FetchSaleItems(int saleId);
        int FetchTotalProductSold();
        List<SaleHistoryModel> GetSalesHistoryWithItems();
    }

    //For product queries
    public interface IProductRepository
    {

    }

}
