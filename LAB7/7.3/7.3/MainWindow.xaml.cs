using OnlineShop.Models;
using OnlineShop.Patterns;
using System.Collections.Generic;
using System.Windows;

namespace OnlineShop
{
    public partial class MainWindow : Window
    {
        private List<Product> _products = new List<Product>();
        private List<Product> _cart = new List<Product>();
        private IOrderBuilder _orderBuilder = new OrderBuilder();

        public MainWindow()
        {
            InitializeComponent();
            LoadProducts();
        }

        private void LoadProducts()
        {
            // Добавление товаров вручную
            _products.Add(new Book { Name = "Книга: 'Грокаем алгоритмы'", Price = 20.99m, Author = "Адитья Бхаргава" });
            _products.Add(new Electronics { Name = "Смартфон Samsung Galaxy S23", Price = 800.99m, Brand = "Samsung" });
            _products.Add(new Electronics { Name = "Ноутбук Asus Vivobook", Price = 1200.99m, Brand = "Asus" });
            _products.Add(new Book { Name = "Книга: '1984'", Price = 15.99m, Author = "Джордж Оруэлл" });
            _products.Add(new Electronics { Name = "Наушники Sony WH-1000XM5", Price = 350.99m, Brand = "Sony" });

            // Привязка списка товаров к ListBox
            ProductListBox.ItemsSource = _products;
        }

        private void AddToCart_Click(object sender, RoutedEventArgs e)
        {
            if (ProductListBox.SelectedItem is Product selectedProduct)
            {
                _cart.Add(selectedProduct);
                _orderBuilder.AddProduct(selectedProduct);
                CartListBox.ItemsSource = null;
                CartListBox.ItemsSource = _cart;
            }
        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            var order = _orderBuilder.GetOrder();
            MessageBox.Show($"Заказ оформлен! Итого: {order.TotalPrice}");
        }
    }
}