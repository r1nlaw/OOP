namespace OnlineShop.Patterns
{
    public interface IShippingStrategy
    {
        decimal CalculateShippingCost(Models.Order order);
    }

    public class StandardShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(Models.Order order)
        {
            return order.TotalPrice * 0.1m;
        }
    }

    public class ExpressShippingStrategy : IShippingStrategy
    {
        public decimal CalculateShippingCost(Models.Order order)
        {
            return order.TotalPrice * 0.2m;
        }
    }
}