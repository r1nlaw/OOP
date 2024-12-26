namespace OnlineShop.Models
{
    public abstract class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

    public class Book : Product
    {
        public string Author { get; set; }
    }

    public class Electronics : Product
    {
        public string Brand { get; set; }
    }
}