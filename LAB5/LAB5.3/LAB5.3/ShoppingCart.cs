using System.Collections.Generic;
using System.Text;

public class ShoppingCart : IShoppingCart
{
    private List<IProduct> products = new List<IProduct>();

    public void AddProduct(IProduct product)
    {
        products.Add(product);
    }

    public void RemoveProduct(IProduct product)
    {
        products.Remove(product);
    }

    public decimal GetTotalPrice()
    {
        decimal total = 0;
        foreach (var product in products)
        {
            total += product.Price;
        }
        return total;
    }

    public string GetCartDetails()
    {
        StringBuilder details = new StringBuilder();
        foreach (var product in products)
        {
            details.AppendLine(product.GetProductDetails());
        }
        details.AppendLine($"Total Price: ${GetTotalPrice()}");
        return details.ToString();
    }
}
