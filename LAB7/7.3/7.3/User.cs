using System.Collections.Generic;

namespace OnlineShop.Models
{
    public class User
    {
        public string Name { get; set; }
        public List<Order> Orders { get; set; } = new List<Order>();
    }
}