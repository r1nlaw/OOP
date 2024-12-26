using System;

namespace OnlineShop.Patterns
{
    public interface IOrderObserver
    {
        void Update(Models.Order order);
    }

    public class UserObserver : IOrderObserver
    {
        private Models.User _user;

        public UserObserver(Models.User user)
        {
            _user = user;
        }

        public void Update(Models.Order order)
        {
            Console.WriteLine($"User {_user.Name}, your order status is: {order.Status}");
        }
    }
}