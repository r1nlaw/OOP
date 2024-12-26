using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.Models
{
    public class Order
    {
        public List<Product> Items { get; set; } = new List<Product>();
        public decimal TotalPrice => Items.Sum(p => p.Price);
        public string Status { get; set; } = "Pending";
    }
}