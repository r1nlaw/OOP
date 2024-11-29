using System;
using System.Windows;
using System.Collections.Generic;

namespace ShoppingCartApp
{
    public partial class MainWindow : Window
    {
        private IShoppingCart cart;
        private IProduct computer1;
        private IProduct computer2;

        public MainWindow()
        {
            InitializeComponent();
            cart = new ShoppingCart();

            // Создаем продукты
            computer1 = new Computer
            {
                Name = "Gaming PC",
                Price = 1200,
                Processor = "Intel i7",
                RAM = 16,
                Storage = 512
            };

            computer2 = new Computer
            {
                Name = "Office PC",
                Price = 800,
                Processor = "Intel i5",
                RAM = 8,
                Storage = 256
            };

            // Добавляем продукты в список
            ProductsListBox.Items.Add(computer1);
            ProductsListBox.Items.Add(computer2);
        }

        // Добавить Gaming PC в корзину
        private void AddGamingPC_Click(object sender, RoutedEventArgs e)
        {
            cart.AddProduct(computer1);
            MessageBox.Show($"Added {computer1.Name} to the cart.");
            UpdateCartDetails();
        }

        // Добавить Office PC в корзину
        private void AddOfficePC_Click(object sender, RoutedEventArgs e)
        {
            cart.AddProduct(computer2);
            MessageBox.Show($"Added {computer2.Name} to the cart.");
            UpdateCartDetails();
        }

        // Удалить выбранный продукт из корзины
        private void RemoveProduct_Click(object sender, RoutedEventArgs e)
        {
            var selectedProduct = ProductsListBox.SelectedItem as IProduct;
            if (selectedProduct != null)
            {
                cart.RemoveProduct(selectedProduct);
                MessageBox.Show($"Removed {selectedProduct.Name} from the cart.");
                UpdateCartDetails();
            }
            else
            {
                MessageBox.Show("Please select a product to remove.");
            }
        }

        // Показать детали корзины
        private void ShowCartDetails_Click(object sender, RoutedEventArgs e)
        {
            CartDetailsTextBox.Text = cart.GetCartDetails();
        }

        // Обновить отображение корзины
        private void UpdateCartDetails()
        {
            CartDetailsTextBox.Text = cart.GetCartDetails();
        }
    }
}
