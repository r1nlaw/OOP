using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows;

namespace HashGenerator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void GenerateHash_Click(object sender, RoutedEventArgs e)
        {
            string fullName = FullNameTextBox.Text;
            if (string.IsNullOrWhiteSpace(fullName))
            {
                HashResultTextBlock.Text = "Пожалуйста, введите ФИО.";
                return;
            }

            string hash = ComputeHash(fullName);
            HashResultTextBlock.Text = $"Хэш: {hash}";
        }

        private string ComputeHash(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder hashString = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashString.Append(b.ToString("x2")); // Форматируем байт в шестнадцатеричный вид
                }

                return hashString.ToString();
            }
        }
    }
}
