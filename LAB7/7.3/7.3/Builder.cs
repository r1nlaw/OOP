namespace OnlineShop.Patterns
{
    public interface IOrderBuilder
    {
        void AddProduct(Models.Product product);
        Models.Order GetOrder();
    }

    public class OrderBuilder : IOrderBuilder
    {
        private Models.Order _order = new Models.Order();

        public void AddProduct(Models.Product product)
        {
            _order.Items.Add(product);
        }

        public Models.Order GetOrder()
        {
            return _order;
        }
    }
}