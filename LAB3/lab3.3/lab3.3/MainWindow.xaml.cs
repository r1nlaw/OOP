using System;
using System.Windows;

namespace IntegerOverflowApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DoMultiply_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int firstNumber = int.Parse(FirstNumberTextBox.Text);
                int secondNumber = int.Parse(SecondNumberTextBox.Text);
                int result;

                // Используем checked блок для обработки переполнения
                checked
                {
                    result = firstNumber * secondNumber;
                }

                ResultTextBlock.Text = $"Результат: {result}";
            }
            catch (OverflowException)
            {
                MessageBox.Show("Произошло переполнение при умножении.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные целые числа.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DoMultiplyUnchecked_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int firstNumber = int.Parse(FirstNumberTextBox.Text);
                int secondNumber = int.Parse(SecondNumberTextBox.Text);
                int result;

                // Используем unchecked блок для игнорирования переполнения
                unchecked
                {
                    result = firstNumber * secondNumber; // Не будет вызывать исключение, даже если произойдет переполнение
                }

                ResultTextBlock.Text = $"Результат (без проверки): {result}";
            }
            catch (FormatException)
            {
                MessageBox.Show("Введите корректные целые числа.", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
