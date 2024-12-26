namespace OnlineShop.Patterns
{
    public interface IProductFactory
    {
        Models.Product CreateProduct();
    }

    public class BookFactory : IProductFactory
    {
        public Models.Product CreateProduct()
        {
            return new Models.Book { Name = "Sample Book", Price = 20, Author = "Author" };
        }
    }

    public class ElectronicsFactory : IProductFactory
    {
        public Models.Product CreateProduct()
        {
            return new Models.Electronics { Name = "Sample Electronics", Price = 100, Brand = "Brand" };
        }
    }
}