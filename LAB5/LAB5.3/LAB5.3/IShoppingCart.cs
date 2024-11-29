public interface IShoppingCart
{
    void AddProduct(IProduct product);
    void RemoveProduct(IProduct product);
    decimal GetTotalPrice();
    string GetCartDetails();
}
